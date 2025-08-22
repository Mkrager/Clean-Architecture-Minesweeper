using Minesweeper.Application.Contracts.Infrastructure;

namespace Minesweeper.Infrastructure.Minesweeper.Solver
{
    public class SimpleLogicStrategy : MoveStrategyBase
    {
        public override async Task<bool> ApplyAsync(Game game, IMinesweeperService service)
        {
            bool moveMade = false;

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    var cell = game.Field[x, y];
                    if (!cell.IsOpened || cell.AdjacentMines == 0) continue;

                    var neighbors = GetNeighbors(game, cell.X, cell.Y);
                    var flagged = neighbors.Count(c => c.HasFlag);
                    var hidden = neighbors.Where(c => !c.IsOpened && !c.HasFlag).ToList();

                    if (cell.AdjacentMines - flagged == hidden.Count && hidden.Count > 0)
                    {
                        foreach (var mine in hidden)
                            await service.ToggleFlagAsync(game.GameId, mine.X, mine.Y);
                        moveMade = true;
                    }
                    else if (flagged == cell.AdjacentMines && hidden.Count > 0)
                    {
                        foreach (var safe in hidden)
                            await service.OpenCellAsync(game.GameId, safe.X, safe.Y);
                        moveMade = true;
                    }
                }
            }

            return moveMade;
        }
    }
}