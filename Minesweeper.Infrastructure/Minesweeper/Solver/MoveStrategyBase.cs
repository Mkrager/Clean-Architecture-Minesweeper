using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Minesweeper.Solver
{
    public abstract class MoveStrategyBase : IMoveStrategy
    {
        protected List<Cell> GetNeighbors(Game game, int x, int y)
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

        public abstract Task<bool> ApplyAsync(Game game, IMinesweeperService service);
    }
}