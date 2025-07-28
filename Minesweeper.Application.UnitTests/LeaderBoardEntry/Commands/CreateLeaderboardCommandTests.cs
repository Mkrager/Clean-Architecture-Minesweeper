using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;
using Minesweeper.Application.UnitTests.Mocks;
using Minesweeper.Domain.Entities;
using Moq;

namespace Minesweeper.Application.UnitTests.LeaderBoardEntry.Commands
{
    public class CreateLeaderboardCommandTests
    {
        private readonly Mock<IAsyncRepository<LeaderboardEntry>> _mockLeaderboardEntryRepository;
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;
        public CreateLeaderboardCommandTests()
        {
            _mockLeaderboardEntryRepository = RepositoryMocks.GetLeaderboardRepository();
            _mockMinesweeperService = RepositoryMocks.GetMinesweeperService();
        }

        [Fact]
        public async Task Should_Create_Leaderboard_Successfully()
        {
            var handler = new CreateLeaderboadEntryCommandHandler(_mockLeaderboardEntryRepository.Object, _mockMinesweeperService.Object);

            var command = new CreateLeaderboadEntryCommand
            {
                GameId = Guid.NewGuid(),
                PlayerName = "123"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);
        }

    }
}
