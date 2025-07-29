using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Application.DTOs;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Minesweeper.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IMinesweeperService> GetMinesweeperService()
        {
            var mockService = new Mock<IMinesweeperService>();

            mockService.Setup(service => service.CreateNewGameAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(Guid.Parse("271c6ab8-20ad-4053-83e0-c69f69c5fc29"));

            mockService.Setup(service => service.GetGameStateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new GameStateDto()
                {
                    Height = 5,
                    Width = 5,
                    Status = 0,
                    GameId = Guid.Parse("fdecf6a7-79ee-4e72-a462-5f42a7a4db87"),
                    Cells = new List<CellDto>
                    {
                        new CellDto
                        {
                            AdjacentMines = 3,
                            HasFlag = false,
                            HasMine = false,
                            IsOpened = false,
                            X = 0,
                            Y = 1
                        }
                    }
                });

            mockService.Setup(service => service.OpenCellAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync(new OpenCellResult()
                {
                    Status = 0,
                    NewlyOpenedCells = new List<CellDto>
                    {
                        new CellDto
                        {
                            AdjacentMines = 1,
                            HasFlag = false,
                            HasMine = false,
                            IsOpened = true,
                            X = 0,
                            Y = 2
                        }
                    }
                });

            mockService.Setup(service => service.ToggleFlagAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync(new ToggleFlagResult()
                {
                    Success = true,
                    UpdatedCell =
                        new CellDto
                        {
                            AdjacentMines = 1,
                            HasFlag = true,
                            HasMine = false,
                            IsOpened = false,
                            X = 0,
                            Y = 2
                        }
                });

            mockService.Setup(service => service.GetGame(It.IsAny<Guid>()))
                .Returns(new Game());



            return mockService;
        }

        public static Mock<IAsyncRepository<LeaderboardEntry>> GetLeaderboardRepository()
        {
            var leaderboards = new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Id = Guid.Parse("cade1266-42b5-43f2-a2fe-01727ffdac39"), PlayerName = "name", GameLevel = GameLevel.Medium },
                new LeaderboardEntry { Id = Guid.Parse("cade1266-42b5-43f2-a2fe-01727ffdac23"), PlayerName = "name2" }
            };

            var mockRepository = new Mock<IAsyncRepository<LeaderboardEntry>>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(leaderboards);

            mockRepository.Setup(r => r.AddAsync(It.IsAny<LeaderboardEntry>()))
                .ReturnsAsync((LeaderboardEntry leaderboardEntry) =>
                {
                    leaderboards.Add(leaderboardEntry);
                    return leaderboardEntry;
                });

            mockRepository
                .Setup(r => r.ListAsync(It.IsAny<Expression<Func<LeaderboardEntry, bool>>>()))
                .Returns<Expression<Func<LeaderboardEntry, bool>>>(predicate =>
                {
                    var compiledPredicate = predicate.Compile();
                    var filtered = leaderboards.Where(compiledPredicate).ToList();
                    return Task.FromResult(filtered);
                });
            return mockRepository;
        }
    }
}
