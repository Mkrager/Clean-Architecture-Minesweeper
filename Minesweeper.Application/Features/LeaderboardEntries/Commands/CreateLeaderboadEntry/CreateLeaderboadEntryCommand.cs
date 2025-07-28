using MediatR;

namespace Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry
{
    public class CreateLeaderboadEntryCommand : IRequest<Guid>
    {
        public Guid GameId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
    }
}
