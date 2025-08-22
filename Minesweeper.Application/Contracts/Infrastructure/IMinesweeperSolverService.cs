namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface IMinesweeperSolverService
    {
        Task SolveAsync(Game game);
    }
}
