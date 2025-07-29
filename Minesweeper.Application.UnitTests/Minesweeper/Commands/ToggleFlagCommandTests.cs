using AutoMapper;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Moq;

namespace Minesweeper.Application.UnitTests.Minesweeper.Commands
{
    public class ToggleFlagCommandTests
    {
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;
        private readonly IMapper _mapper;

        public ToggleFlagCommandTests()
        {
            _mockMinesweeperService = RepositoryMocks.GetMinesweeperService();
            var configuratinProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuratinProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Toggle_Flag_Successfully_ReturnsToggleFlagResponse()
        {
            var handler = new ToggleFlagCommandHandler(_mapper, _mockMinesweeperService.Object);

            var command = new ToggleFlagCommand
            {
                GameId = Guid.Parse("dd58fe30-c9b0-4999-9043-4a8af95d6046"),
                X = 0,
                Y = 2
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<ToggleFlagResponse>(result);
            Assert.True(result.Success);
            Assert.NotNull(result);
            Assert.NotNull(result.UpdatedCell);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmptyGuidId()
        {
            var validator = new ToggleFlagCommandValidator();
            var query = new ToggleFlagCommand
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
