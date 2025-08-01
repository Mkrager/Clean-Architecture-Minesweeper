using AutoMapper;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Moq;

namespace Minesweeper.Application.UnitTests.Minesweeper.Queries
{
    public class GetGameStateQueryTests
    {
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;
        private readonly IMapper _mapper;

        public GetGameStateQueryTests()
        {
            _mockMinesweeperService = MinesweeperServiceMock.GetMinesweeperService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetGameState_ReturnsGameState()
        {
            var handler = new GetGameStateQueryHandler(_mockMinesweeperService.Object, _mapper);

            var result = await handler.Handle(new GetGameStateQuery() { GameId = Guid.Parse("a54043ff-59d4-46a4-9f72-1c0b0ed1ba55") }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.Cells);
            Assert.IsType<GameStateVm>(result);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmptyGuidId()
        {
            var validator = new GetGameStateQueryValidator();
            var query = new GetGameStateQuery
            {
                GameId = Guid.Empty,
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "GameId");
        }
    }
}
