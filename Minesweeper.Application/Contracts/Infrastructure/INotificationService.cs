using Minesweeper.Application.DTOs;

namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface INotificationService
    {
        Task NotifyAsync(GameStateDto gameStateDto);
    }
}
