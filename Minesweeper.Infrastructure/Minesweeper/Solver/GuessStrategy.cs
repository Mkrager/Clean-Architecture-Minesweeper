using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Minesweeper.Solver
{
    public class GuessStrategy : MoveStrategyBase
    {
        public override Task<bool> ApplyAsync(Game game, IMinesweeperService service)
        {
            var guess = GetBestGuessCell(game);
            if (guess == null) return Task.FromResult(false);

            return service.OpenCellAsync(game.GameId, guess.X, guess.Y)
                .ContinueWith(_ => true);
        }

        private Cell? GetBestGuessCell(Game game)
        {
            var candidates = new List<(Cell cell, double risk)>();

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    var cell = game.Field[x, y];
                    if (cell.IsOpened || cell.HasFlag) continue;

                    var neighbors = GetNeighbors(game, cell.X, cell.Y).Where(c => c.IsOpened).ToList();
                    if (neighbors.Count == 0)
                    {
                        candidates.Add((cell, 0.5));
                        continue;
                    }

                    double totalRisk = 0;
                    int count = 0;
                    foreach (var n in neighbors)
                    {
                        var adjacent = GetNeighbors(game, n.X, n.Y);
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

            if (candidates.Count == 0) return null;

            return candidates.OrderBy(c => c.risk).First().cell;
        }
    }
}