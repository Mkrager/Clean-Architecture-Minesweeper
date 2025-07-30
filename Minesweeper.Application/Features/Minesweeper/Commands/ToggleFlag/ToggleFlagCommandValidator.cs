using FluentValidation;

namespace Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag
{
    public class ToggleFlagCommandValidator : AbstractValidator<ToggleFlagCommand>
    {
        public ToggleFlagCommandValidator()
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
