using MediatR;

namespace Minesweeper.Application.Features.Solver.Command.Solve
{
    public class SolveCommand : IRequest<SolveCommandResponse>
    {
        public Guid GameId { get; set; }
    }
}
