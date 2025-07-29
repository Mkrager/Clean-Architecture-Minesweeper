using AutoMapper;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Moq;

namespace Minesweeper.Application.UnitTests.Minesweeper.Commands
{
    public class OpenCellCommandTests
    {
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;
        private readonly IMapper _mapper;

        public OpenCellCommandTests()
        {
            _mockMinesweeperService = RepositoryMocks.GetMinesweeperService();
            var configuratinProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuratinProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Open_Cell_Successfully_ReturnsOpenCellResponse()
        {
            var handler = new OpenCellCommandHandler(_mapper, _mockMinesweeperService.Object);

            var command = new OpenCellCommand
            {
                GameId = Guid.Parse("dd58fe30-c9b0-4999-9043-4a8af95d6046"),
                X = 0,
                Y = 2
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<OpenCellResponse>(result);
            Assert.NotNull(result);
            Assert.NotNull(result.NewlyOpenedCells);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmptyGuidId()
        {
            var validator = new OpenCellCommandValidator();
            var query = new OpenCellCommand
            {
                GameId = Guid.Empty,
                X = 0,
                Y = 1
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "GameId");
        }
    }
}
