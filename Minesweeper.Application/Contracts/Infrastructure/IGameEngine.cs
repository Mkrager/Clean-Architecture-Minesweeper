using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface IGameEngine
    {
        void Initialize(Game game);
        List<(int X, int Y, Cell cell)> OpenCell(Game game, int x, int y);
        Cell ToggleFlag(Game game, int x, int y);
    }
}
