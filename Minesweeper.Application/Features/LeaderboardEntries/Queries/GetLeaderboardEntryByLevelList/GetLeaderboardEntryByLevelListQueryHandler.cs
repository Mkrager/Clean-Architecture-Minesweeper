using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryByLevelList
{
    public class GetLeaderboardEntryByLevelListQueryHandler : IRequestHandler<GetLeaderboardEntryByLevelListQuery, List<LeaderboardEntryByLevelListVm>>
    {
        private readonly IAsyncRepository<LeaderboardEntry> _leaderboardEntryRepository;
        private readonly IMapper _mapper;
        public GetLeaderboardEntryByLevelListQueryHandler(IAsyncRepository<LeaderboardEntry> leaderboardEntryRepository, IMapper mapper)
        {
            _leaderboardEntryRepository = leaderboardEntryRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaderboardEntryByLevelListVm>> Handle(GetLeaderboardEntryByLevelListQuery request, CancellationToken cancellationToken)
        {
            var leaderboard = (await _leaderboardEntryRepository.ListAsync(e => e.GameLevel == request.GameLevel))
                    .OrderBy(e => e.Time);

            return _mapper.Map<List<LeaderboardEntryByLevelListVm>>(leaderboard);
        }
    }
}
