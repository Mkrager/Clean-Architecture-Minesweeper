using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class MinesweeperSolver : IMinesweeperSolverService
    {
        private Game _game = null!;
        private readonly IMinesweeperService _minesweeperService;

        public MinesweeperSolver(IMinesweeperService minesweeperService)
        {
            _minesweeperService = minesweeperService;
        }

        public void Solve(Game game)
        {
            _game = game;

            while (_game.Status == GameStatus.InProgress)
            {
                var moveMade = false;

                for (int x = 0; x < _game.Width; x++)
                {
                    for (int y = 0; y < _game.Height; y++)
                    {
                        var cell = _game.Field[x, y];

                        if (!cell.IsOpened || cell.AdjacentMines == 0)
                            continue;

                        var neighbors = GetNeighbors(cell.X, cell.Y);
                        var flagged = neighbors.Count(c => c.HasFlag);
                        var hidden = neighbors.Where(c => !c.IsOpened && !c.HasFlag).ToList();

                        if (cell.AdjacentMines - flagged == hidden.Count && hidden.Count > 0)
                        {
                            foreach (var mine in hidden)
                                _minesweeperService.ToggleFlagAsync(game.GameId, mine.X, mine.Y).Wait();
                            moveMade = true;
                        }
                        else if (flagged == cell.AdjacentMines && hidden.Count > 0)
                        {
                            foreach (var safe in hidden)
                                _minesweeperService.OpenCellAsync(game.GameId, safe.X, safe.Y).Wait();
                            moveMade = true;
                        }
                    }
                }

                if (ApplySubsetLogic(game.GameId))
                    moveMade = true;

                if (!moveMade)
                {
                    var guess = GetBestGuessCell();
                    if (guess == null)
                        break;

                    _minesweeperService.OpenCellAsync(game.GameId, guess.X, guess.Y).Wait();
                }
            }
        }

        private List<Cell> GetNeighbors(int x, int y)
        {
            var neighbors = new List<Cell>();

            foreach (var (dx, dy) in GetNeighborOffsets())
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && ny >= 0 && nx < _game.Width && ny < _game.Height)
                    neighbors.Add(_game.Field[nx, ny]);
            }

            return neighbors;
        }

        private List<(int dx, int dy)> GetNeighborOffsets()
        {
            return new List<(int, int)>
            {
                (-1, -1), (0, -1), (1, -1),
                (-1, 0),           (1, 0),
                (-1, 1),  (0, 1),  (1, 1)
            };
        }

        private Cell? GetBestGuessCell()
        {
            var candidates = new List<(Cell cell, double risk)>();

            for (int x = 0; x < _game.Width; x++)
            {
                for (int y = 0; y < _game.Height; y++)
                {
                    var cell = _game.Field[x, y];
                    if (cell.IsOpened || cell.HasFlag) continue;

                    var neighbors = GetNeighbors(cell.X, cell.Y).Where(c => c.IsOpened).ToList();
                    if (neighbors.Count == 0)
                    {
                        candidates.Add((cell, 0.5));
                        continue;
                    }

                    double totalRisk = 0;
                    int count = 0;
                    foreach (var n in neighbors)
                    {
                        var adjacent = GetNeighbors(n.X, n.Y);
                        int flagged = adjacent.Count(c => c.HasFlag);
                        int hidden = adjacent.Count(c => !c.IsOpened && !c.HasFlag);
                        int minesLeft = n.AdjacentMines - flagged;

                        if (hidden > 0 && adjacent.Contains(cell))
                        {
                            totalRisk += (double)minesLeft / hidden;
                            count++;
                        }
                    }

                    double avgRisk = count > 0 ? totalRisk / count : 0.5;
                    candidates.Add((cell, avgRisk));
                }
            }

            if (candidates.Count == 0)
                return null;

            return candidates.OrderBy(c => c.risk).First().cell;
        }

        private bool ApplySubsetLogic(Guid gameId)
        {
            var moveMade = false;

            var openedCells = new List<(Cell cell, List<Cell> hidden, int minesLeft)>();

            for (int x = 0; x < _game.Width; x++)
            {
                for (int y = 0; y < _game.Height; y++)
                {
                    var cell = _game.Field[x, y];
                    if (!cell.IsOpened || cell.AdjacentMines == 0)
                        continue;

                    var neighbors = GetNeighbors(cell.X, cell.Y);
                    var flagged = neighbors.Count(n => n.HasFlag);
                    var hidden = neighbors.Where(n => !n.IsOpened && !n.HasFlag).ToList();
                    int minesLeft = cell.AdjacentMines - flagged;

                    if (hidden.Count > 0 && minesLeft >= 0)
                        openedCells.Add((cell, hidden, minesLeft));
                }
            }

            for (int i = 0; i < openedCells.Count; i++)
            {
                for (int j = 0; j < openedCells.Count; j++)
                {
                    if (i == j) continue;

                    var (cellA, hiddenA, minesA) = openedCells[i];
                    var (cellB, hiddenB, minesB) = openedCells[j];

                    if (!hiddenB.All(hiddenA.Contains)) continue;

                    var diff = hiddenA.Except(hiddenB).ToList();
                    int mineDiff = minesA - minesB;

                    if (mineDiff == diff.Count && mineDiff > 0)
                    {
                        foreach (var cell in diff)
                            _minesweeperService.ToggleFlagAsync(gameId, cell.X, cell.Y).Wait();
                        moveMade = true;
                    }
                    else if (mineDiff == 0 && diff.Count > 0)
                    {
                        foreach (var cell in diff)
                            _minesweeperService.OpenCellAsync(gameId, cell.X, cell.Y).Wait();
                        moveMade = true;
                    }
                }
            }

            return moveMade;
        }
    }
}