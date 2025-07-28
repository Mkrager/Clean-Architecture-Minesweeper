using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList
{
    public class GetLeaderboardEntryListQueryHandler : IRequestHandler<GetLeaderboardEntryListQuery, List<LeaderboardEntryListVm>>
    {
        private readonly IAsyncRepository<LeaderboardEntry> _leaderboardEntryRepository;
        private readonly IMapper _mapper;
        public GetLeaderboardEntryListQueryHandler(IAsyncRepository<LeaderboardEntry> leaderboardEntryRepository, IMapper mapper)
        {
            _leaderboardEntryRepository = leaderboardEntryRepository;
            _mapper = mapper;
        }
        public async Task<List<LeaderboardEntryListVm>> Handle(GetLeaderboardEntryListQuery request, CancellationToken cancellationToken)
        {
            var leaderboard = (await _leaderboardEntryRepository.ListAllAsync()).OrderBy(e => e.Time);

            return _mapper.Map<List<LeaderboardEntryListVm>>(leaderboard);
        }
    }
}
