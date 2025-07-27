namespace Minesweeper.Application.Contracts.Infrastructure
{
    public interface INotificationService
    {
        Task NotifyAsync<T>(T result);
    }
}
