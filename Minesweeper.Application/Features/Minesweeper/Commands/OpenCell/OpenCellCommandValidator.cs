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
                .NotEmpty().WithMessage("Empty X");
            RuleFor(r => r.Y)
                .NotEmpty().WithMessage("Empty Y");
        }
    }
}
