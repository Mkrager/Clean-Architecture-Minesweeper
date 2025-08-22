using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class SimpleLogicStrategy : IMoveStrategy
    {
        public async Task<bool> ApplyAsync(Game game, IMinesweeperService service)
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

        private List<Cell> GetNeighbors(Game game, int x, int y)
        {
            var neighbors = new List<Cell>();
            var offsets = new (int dx, int dy)[]
            {
                (-1,-1),(0,-1),(1,-1),
                (-1,0),       (1,0),
                (-1,1),(0,1),(1,1)
            };

            foreach (var (dx, dy) in offsets)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && ny >= 0 && nx < game.Width && ny < game.Height)
                    neighbors.Add(game.Field[nx, ny]);
            }

            return neighbors;
        }
    }
}