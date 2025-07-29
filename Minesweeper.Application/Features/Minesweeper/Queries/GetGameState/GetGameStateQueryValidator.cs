using FluentValidation;

namespace Minesweeper.Application.Features.Minesweeper.Queries.GetGameState
{
    public class GetGameStateQueryValidator : AbstractValidator<GetGameStateQuery>
    {
        public GetGameStateQueryValidator()
        {
            RuleFor(r => r.GameId)
                .NotEmpty().WithMessage("Empty GameId");
        }
    }
}
