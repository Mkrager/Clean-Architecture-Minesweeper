using Minesweeper.Application.Contracts.Infrastructure;
using Moq;

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

            return mockService;
        }
    }
}
