using Minesweeper.App.ViewModels;

namespace Minesweeper.App.Contracts
{
    public interface IMinesweeperService
    {
        Task<Guid> CreateGame(CreateGameRequest createGameRequest);
        Task<GameStateViewModel> GetGameState(Guid gameId);
    }
}
