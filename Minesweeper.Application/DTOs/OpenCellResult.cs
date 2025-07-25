using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.DTOs
{
    public class OpenCellResult
    {
        public GameStatus Status { get; set; }
        public List<CellDto>? NewlyOpenedCells { get; set; }
    }
}
