using FluentValidation;

namespace Minesweeper.Application.Features.Minesweeper.Commands.OpenCell
{
    public class OpenCellCommandValidator : AbstractValidator<OpenCellCommand>
    {
        public OpenCellCommandValidator()
        {
            RuleFor(r => r.GameId)
                .NotEmpty().WithMessage("Empty game");
            RuleFor(r => r.X)
                .GreaterThanOrEqualTo(0).WithMessage("X must be 0 or greater.");
            RuleFor(r => r.Y)
                .GreaterThanOrEqualTo(0).WithMessage("X must be 0 or greater.");
        }
    }
}
