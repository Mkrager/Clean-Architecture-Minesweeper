using Microsoft.AspNetCore.SignalR;

namespace Minesweeper.Api.Hubs
{
    public class NotificationHub : Hub
    {
        public Task JoinGameGroup(string gameId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public Task LeaveGameGroup(string gameId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }
    }
}
