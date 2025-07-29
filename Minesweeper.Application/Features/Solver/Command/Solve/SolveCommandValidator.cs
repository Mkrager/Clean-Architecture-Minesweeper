using FluentValidation;

namespace Minesweeper.Application.Features.Solver.Command.Solve
{
    public class SolveCommandValidator : AbstractValidator<SolveCommand>
    {
        public SolveCommandValidator()
        {
            RuleFor(r => r.GameId)
                .NotEmpty().WithMessage("Empty GameId");
        }
    }
}
