using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.Exceptions;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry
{
    public class CreateLeaderboadEntryCommandHandler : IRequestHandler<CreateLeaderboadEntryCommand, Guid>
    {
        private readonly IAsyncRepository<LeaderboardEntry> _leaderboardEntriesRepository;
        private readonly IMinesweeperService _minesweeperService;

        public CreateLeaderboadEntryCommandHandler(IAsyncRepository<LeaderboardEntry> leaderboardEntriesRepository, IMinesweeperService minesweeperService)
        {
            _leaderboardEntriesRepository = leaderboardEntriesRepository;
            _minesweeperService = minesweeperService;
        }
        public async Task<Guid> Handle(CreateLeaderboadEntryCommand request, CancellationToken cancellationToken)
        {
            var game = _minesweeperService.GetGame(request.GameId);

            if (game == null)
                throw new NotFoundException(nameof(Game), request.GameId);

            var duration = game.EndTime - game.StartTime;

            var leaderboadEntry = await _leaderboardEntriesRepository.AddAsync(new LeaderboardEntry()
            {
                PlayerName = request.PlayerName,
                Time = duration,
            });

            return leaderboadEntry.Id;
        }
    }
}
