using Minesweeper.Application.Contracts.Infrastructure;
using Moq;

namespace Minesweeper.Application.UnitTests.Mocks
{
    public class MinesweeperSolverServiceMock
    {
        public static Mock<IMinesweeperSolverService> GetMinesweeperSolverService()
        {
            var mockRepository = new Mock<IMinesweeperSolverService>();

            mockRepository.Setup(r => r.Solve(It.IsAny<Game>()));

            return mockRepository;
        }
    }
}