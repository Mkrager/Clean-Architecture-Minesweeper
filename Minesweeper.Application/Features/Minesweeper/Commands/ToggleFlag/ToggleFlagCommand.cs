using MediatR;

namespace Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag
{
    public class ToggleFlagCommand : IRequest<ToggleFlagResponse>
    {
        public Guid GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
