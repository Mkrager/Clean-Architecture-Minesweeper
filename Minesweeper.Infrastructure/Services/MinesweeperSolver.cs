using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class MinesweeperSolver : IMinesweeperSolverService
    {
        private Game _game = null!;

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
                                mine.HasFlag = true;
                            moveMade = true;
                        }
                        else if (flagged == cell.AdjacentMines && hidden.Count > 0)
                        {
                            foreach (var safe in hidden)
                                OpenCell(safe);
                            moveMade = true;
                        }
                    }
                }

                if (ApplySubsetLogic())
                    moveMade = true;

                if (!moveMade)
                {
                    var guess = GetBestGuessCell();
                    if (guess == null)
                        break;

                    OpenCell(guess);
                }
            }
        }

        private void OpenCell(Cell cell)
        {
            if (cell.IsOpened || cell.HasFlag)
                return;

            cell.IsOpened = true;

            if (cell.HasMine)
            {
                _game.Status = GameStatus.Lost;
                _game.EndTime = DateTime.Now;
                return;
            }

            if (cell.AdjacentMines == 0)
            {
                foreach (var (dx, dy) in GetNeighborOffsets())
                {
                    int nx = cell.X + dx;
                    int ny = cell.Y + dy;
                    if (nx >= 0 && ny >= 0 && nx < _game.Width && ny < _game.Height)
                    {
                        var neighbor = _game.Field[nx, ny];
                        if (!neighbor.IsOpened)
                            OpenCell(neighbor);
                    }
                }
            }

            if (CheckWin())
            {
                _game.Status = GameStatus.Won;
                _game.EndTime = DateTime.Now;
            }
        }

        private bool CheckWin()
        {
            for (int x = 0; x < _game.Width; x++)
            {
                for (int y = 0; y < _game.Height; y++)
                {
                    var cell = _game.Field[x, y];
                    if (!cell.HasMine && !cell.IsOpened)
                        return false;
                }
            }
            return true;
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

        private bool ApplySubsetLogic()
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
                            cell.HasFlag = true;
                        moveMade = true;
                    }
                    else if (mineDiff == 0 && diff.Count > 0)
                    {
                        foreach (var cell in diff)
                            OpenCell(cell);
                        moveMade = true;
                    }
                }
            }

            return moveMade;
        }
    }
}
