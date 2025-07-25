using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.Minesweeper.Queries.GetGameState
{
    public class GameStateVm
    {
        public Guid GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GameStatus Status { get; set; }
        public List<GameStateCellDto> Cells { get; set; } = new();
    }
}
