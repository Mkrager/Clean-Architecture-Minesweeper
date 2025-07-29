using FluentValidation;

namespace Minesweeper.Application.Features.Minesweeper.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(r => r.Height)
                .GreaterThan(0).WithMessage("Height should be rather than 0");

            RuleFor(r => r.Width)
                .GreaterThan(0).WithMessage("Width should be rather than 0");

            RuleFor(r => r.TotalMines)
                .GreaterThan(0).WithMessage("TotalMines should be rather than 0");
        }
    }
}
