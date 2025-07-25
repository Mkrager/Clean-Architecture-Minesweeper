using MediatR;

namespace Minesweeper.Application.Features.Minesweeper.Queries.GetGameState
{
    public class GetGameStateQuery : IRequest<GameStateVm>
    {
        public Guid GameId { get; set; }
    }
}
