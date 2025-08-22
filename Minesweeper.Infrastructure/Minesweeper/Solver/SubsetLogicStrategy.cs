using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Minesweeper.Solver
{
    public class SubsetLogicStrategy : MoveStrategyBase
    {
        public override async Task<bool> ApplyAsync(Game game, IMinesweeperService service)
        {
            bool moveMade = false;
            var openedCells = new List<(Cell cell, List<Cell> hidden, int minesLeft)>();

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    var cell = game.Field[x, y];
                    if (!cell.IsOpened || cell.AdjacentMines == 0) continue;

                    var neighbors = GetNeighbors(game, cell.X, cell.Y);
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
                        foreach (var c in diff)
                            await service.ToggleFlagAsync(game.GameId, c.X, c.Y);
                        moveMade = true;
                    }
                    else if (mineDiff == 0 && diff.Count > 0)
                    {
                        foreach (var c in diff)
                            await service.OpenCellAsync(game.GameId, c.X, c.Y);
                        moveMade = true;
                    }
                }
            }

            return moveMade;
        }
    }
}