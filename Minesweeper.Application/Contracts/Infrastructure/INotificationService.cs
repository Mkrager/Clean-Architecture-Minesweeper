namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface INotificationService
    {
        Task NotifyAsync<T>(Guid gameId, T result);
    }
}
