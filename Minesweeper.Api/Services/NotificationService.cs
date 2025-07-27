using Microsoft.AspNetCore.SignalR;
using Minesweeper.Api.Hubs;
using Minesweeper.Application.Contracts.Infrastructure;
namespace Minesweeper.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyAsync<T>(T result)
        {
            return _hubContext.Clients.All.SendAsync("GameStateUpdated", result);
        }
    }
}
