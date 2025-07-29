using FluentValidation;

namespace Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry
{
    public class CreateLeaderboadEntryCommandValidator : AbstractValidator<CreateLeaderboadEntryCommand>
    {
        public CreateLeaderboadEntryCommandValidator()
        {
            RuleFor(r => r.PlayerName)
                .NotEmpty().WithMessage("Empty PlayerName");
            RuleFor(r => r.GameId)
                .NotEmpty().WithMessage("Empty GameId");
        }
    }
}
