using Minesweeper.Application.DTOs;

namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface IMinesweeperService
    {
        Task<Guid> CreateNewGameAsync(int width, int height, int mines);
        Task<OpenCellResult> OpenCellAsync(Guid gameId, int x, int y);
        Task<ToggleFlagResult> ToggleFlagAsync(Guid gameId, int x, int y);
        Task<GameStateDto> GetGameStateAsync(Guid gameId);
    }
}
