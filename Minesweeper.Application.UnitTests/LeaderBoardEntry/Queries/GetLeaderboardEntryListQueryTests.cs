using AutoMapper;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Minesweeper.Domain.Entities;
using Moq;

namespace Minesweeper.Application.UnitTests.LeaderBoardEntry.Queries
{
    public class GetLeaderboardEntryListQueryTests
    {
        private readonly Mock<IAsyncRepository<LeaderboardEntry>> _mockLeaderboardEntryRepository;
        private readonly IMapper _mapper;

        public GetLeaderboardEntryListQueryTests()
        {
            _mockLeaderboardEntryRepository = RepositoryMocks.GetLeaderboardRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetLeaderboardEntry_ReturnsLeaderboardEntry()
        {
            var handler = new GetLeaderboardEntryListQueryHandler(_mockLeaderboardEntryRepository.Object, _mapper);

            var result = await handler.Handle(new GetLeaderboardEntryListQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<LeaderboardEntryListVm>>(result);
        } 
    }
}
