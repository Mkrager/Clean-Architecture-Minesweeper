﻿using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry;
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
            _mockLeaderboardEntryRepository = LeaderboardRepositoryMock.GetLeaderboardRepository();
            _mockMinesweeperService = MinesweeperServiceMock.GetMinesweeperService();
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

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmptyGuidId()
        {
            var validator = new CreateLeaderboadEntryCommandValidator();
            var query = new CreateLeaderboadEntryCommand
            {
                GameId = Guid.Empty,
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "GameId");
        }
    }
}
