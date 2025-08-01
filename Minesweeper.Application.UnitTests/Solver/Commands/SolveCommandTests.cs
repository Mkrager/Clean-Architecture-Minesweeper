using AutoMapper;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;
using Minesweeper.Application.Features.Solver.Command.Solve;
using Minesweeper.Application.Profiles;
using Minesweeper.Application.UnitTests.Mocks;
using Moq;

namespace Minesweeper.Application.UnitTests.Solver.Commands
{
    public class SolveCommandTests
    {
        private readonly Mock<IMinesweeperService> _mockMinesweeperService;
        private readonly Mock<IMinesweeperSolverService> _mockMinesweeperSolverService;
        private readonly IMapper _mapper;

        public SolveCommandTests()
        {
            _mockMinesweeperService = MinesweeperServiceMock.GetMinesweeperService();
            _mockMinesweeperSolverService = MinesweeperSolverServiceMock.GetMinesweeperSolverService();
            var configuratinProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuratinProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Solve_Game_Successfully()
        {
            var handler = new SolveCommandHandler(_mockMinesweeperSolverService.Object, _mockMinesweeperService.Object, _mapper);

            var command = new SolveCommand
            {
                GameId = Guid.Parse("b688451c-fc70-40e3-9eab-6a7b111eb82b"),
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<SolveCommandResponse>(result);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenEmptyGuidId()
        {
            var validator = new SolveCommandValidator();
            var query = new SolveCommand
            {
                GameId = Guid.Empty
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "GameId");
        }

    }
}
