using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.DTOs
{
    public class GameStateDto
    {
        public Guid GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GameStatus Status { get; set; }
        public List<CellDto>? Cells { get; set; }
    }
}
