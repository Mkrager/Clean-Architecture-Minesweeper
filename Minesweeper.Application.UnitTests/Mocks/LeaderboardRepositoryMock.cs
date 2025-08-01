using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Minesweeper.Application.UnitTests.Mocks
{
    public class LeaderboardRepositoryMock
    {
        public static Mock<IAsyncRepository<LeaderboardEntry>> GetLeaderboardRepository()
        {
            var leaderboards = new List<LeaderboardEntry>
            {
                new LeaderboardEntry
                {
                    Id = Guid.Parse("cade1266-42b5-43f2-a2fe-01727ffdac39"),
                    PlayerName = "name",
                    GameLevel = GameLevel.Medium
                },
                new LeaderboardEntry
                {
                    Id = Guid.Parse("cade1266-42b5-43f2-a2fe-01727ffdac23"),
                    PlayerName = "name2"
                }
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