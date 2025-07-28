using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;
using Minesweeper.Application.UnitTests.Mocks;
using Moq;

namespace Minesweeper.Application.UnitTests.Minesweeper.Commands
{
    public class CreateGameCommandTests
    {
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;

        public CreateGameCommandTests()
        {
            _mockMinesweeperService = RepositoryMocks.GetMinesweeperService();
        }

        [Fact]
        public async Task Should_Create_Game_Successfully()
        {
            var handler = new CreateGameCommandHandler(_mockMinesweeperService.Object);

            var command = new CreateGameCommand
            {
                Height = 5,
                TotalMines = 10,
                Width = 5
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);
        }
    }
}
