using Minesweeper.App.ViewModels;

namespace Minesweeper.App.Contracts
{
    public interface IMinesweeperService
    {
        Task<Guid> CreateSmallGame();
        Task<GameStateViewModel> GetGameState(Guid gameId);
        Task<OpenCellVm> OpenCell(OpenCellRequest openCellRequest);
    }
}
