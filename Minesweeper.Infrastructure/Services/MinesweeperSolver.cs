using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class MinesweeperSolver : IMinesweeperSolver
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

                        var neighbors = GetNeighbors(x, y);
                        var flagged = neighbors.Count(c => c.HasFlag);
                        var hidden = neighbors.Where(c => !c.IsOpened && !c.HasFlag).ToList();

                        if (cell.AdjacentMines - flagged == hidden.Count && hidden.Count > 0)
                        {
                            foreach (var mine in hidden)
                                mine.HasFlag = true;
                            moveMade = true;
                        }

                        if (flagged == cell.AdjacentMines && hidden.Count > 0)
                        {
                            foreach (var safe in hidden)
                                OpenCell(safe);
                            moveMade = true;
                        }
                    }
                }

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
                var coords = GetCoords(cell);
                foreach (var (x, y) in coords)
                {
                    foreach (var neighbor in GetNeighbors(x, y))
                    {
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

        private List<Cell> GetNeighbors(int x, int y)
        {
            var neighbors = new List<Cell>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < _game.Width && ny >= 0 && ny < _game.Height)
                    {
                        neighbors.Add(_game.Field[nx, ny]);
                    }
                }
            }

            return neighbors;
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

        private List<(int x, int y)> GetCoords(Cell target)
        {
            var list = new List<(int, int)>();
            for (int x = 0; x < _game.Width; x++)
            {
                for (int y = 0; y < _game.Height; y++)
                {
                    if (_game.Field[x, y] == target)
                        list.Add((x, y));
                }
            }
            return list;
        }

        private List<Cell> GetNeighborsForCell(Cell cell)
        {
            var coords = GetCoords(cell);
            var neighbors = new List<Cell>();
            foreach (var (x, y) in coords)
            {
                neighbors.AddRange(GetNeighbors(x, y));
            }
            return neighbors.Distinct().ToList();
        }

        private Cell? GetBestGuessCell()
        {
            var candidates = new List<(Cell cell, double risk)>();

            for (int x = 0; x < _game.Width; x++)
            {
                for (int y = 0; y < _game.Height; y++)
                {
                    var cell = _game.Field[x, y];
                    if (cell.IsOpened || cell.HasFlag)
                        continue;

                    var neighbors = GetNeighbors(x, y).Where(c => c.IsOpened).ToList();
                    if (neighbors.Count == 0)
                    {
                        candidates.Add((cell, 0.5));
                        continue;
                    }

                    double riskSum = 0;
                    int count = 0;

                    foreach (var n in neighbors)
                    {
                        var nNeighbors = GetNeighborsForCell(n);
                        int flagged = nNeighbors.Count(c => c.HasFlag);
                        int hidden = nNeighbors.Count(c => !c.IsOpened && !c.HasFlag);
                        int minesLeft = n.AdjacentMines - flagged;

                        if (hidden > 0 && nNeighbors.Contains(cell))
                        {
                            double cellRisk = (double)minesLeft / hidden;
                            riskSum += cellRisk;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        double avgRisk = riskSum / count;
                        candidates.Add((cell, avgRisk));
                    }
                    else
                    {
                        candidates.Add((cell, 0.5));
                    }
                }
            }

            if (candidates.Count == 0)
                return null;

            return candidates.OrderBy(c => c.risk).First().cell;
        }
    }
}
