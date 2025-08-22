namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface IMoveStrategy
    {
        Task<bool> ApplyAsync(Game game, IMinesweeperService service);
    }
}
