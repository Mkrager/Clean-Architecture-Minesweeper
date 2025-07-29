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
                .NotEmpty().WithMessage("Empty X");
            RuleFor(r => r.Y)
                .NotEmpty().WithMessage("Empty Y");
        }
    }
}
