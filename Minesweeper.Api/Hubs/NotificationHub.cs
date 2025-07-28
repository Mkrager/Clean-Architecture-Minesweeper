using MediatR;
using Microsoft.AspNetCore.SignalR;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;

namespace Minesweeper.Api.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IMediator _mediator;

        public NotificationHub(IMediator mediator)
        {
            _mediator = mediator;   
        }
        public Task JoinGameGroup(string gameId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public Task LeaveGameGroup(string gameId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task OpenCell(Guid gameId, int x, int y)
        {
            var result = await _mediator.Send(new OpenCellCommand()
            {
                GameId = gameId,
                X = x,
                Y = y
            });

            await Clients.Group(gameId.ToString()).SendAsync("GameStateUpdated", result);
        }

        public async Task ToggleFlag(Guid gameId, int x, int y)
        {
            var result = await _mediator.Send(new ToggleFlagCommand()
            {
                GameId = gameId,
                X = x,
                Y = y
            });

            await Clients.Group(gameId.ToString()).SendAsync("GameStateUpdated", result);
        }

        public async Task<GameStateVm> GetGameState(Guid gameId)
        {
            var result = await _mediator.Send(new GetGameStateQuery { GameId = gameId });
            return result;
        }
    }
}
