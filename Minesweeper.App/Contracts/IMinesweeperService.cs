namespace Minesweeper.App.Contracts
{
    public interface IMinesweeperService
    {
        Task<Guid> CreateSmallGame();
    }
}
