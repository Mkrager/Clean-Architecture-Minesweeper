using AutoMapper;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryByLevelList;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Minesweeper.Domain.Entities;
using Moq;

namespace Minesweeper.Application.UnitTests.LeaderBoardEntry.Queries
{
    public class GetLeaderboardEntryByLevelListQueryTests
    {
        private readonly Mock<IAsyncRepository<LeaderboardEntry>> _mockLeaderboardEntryRepository;
        private readonly IMapper _mapper;

        public GetLeaderboardEntryByLevelListQueryTests()
        {
            _mockLeaderboardEntryRepository = RepositoryMocks.GetLeaderboardRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetLeaderboardEntryByLevel_ReturnsLeaderboardEntryLevel()
        {
            var handler = new GetLeaderboardEntryByLevelListQueryHandler(_mockLeaderboardEntryRepository.Object, _mapper);

            var result = await handler.Handle(new GetLeaderboardEntryByLevelListQuery() { GameLevel = GameLevel.Medium }, CancellationToken.None);

            var actual = result.FirstOrDefault(r => r.GameLevel == GameLevel.Medium);

            Assert.Equal(GameLevel.Medium, actual?.GameLevel);
            Assert.Single(result);
            Assert.NotNull(result);
            Assert.IsType<List<LeaderboardEntryByLevelListVm>>(result);
        }

        [Fact]
        public async Task Handler_ReceivesCorrectGameLevel()
        {
            var expectedLevel = GameLevel.Medium;

            var handler = new GetLeaderboardEntryByLevelListQueryHandler(_mockLeaderboardEntryRepository.Object, _mapper);


            var query = new GetLeaderboardEntryByLevelListQuery
            {
                GameLevel = expectedLevel
            };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.All(result, r => Assert.Equal(expectedLevel, r.GameLevel));
        }
    }
}
