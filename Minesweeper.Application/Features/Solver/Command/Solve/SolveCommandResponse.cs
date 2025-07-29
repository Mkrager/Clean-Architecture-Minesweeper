using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.Solver.Command.Solve
{
    public class SolveCommandResponse
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public GameStatus Status { get; set; }
        public List<GameStateCellDto> Cells { get; set; } = new();
    }
}
