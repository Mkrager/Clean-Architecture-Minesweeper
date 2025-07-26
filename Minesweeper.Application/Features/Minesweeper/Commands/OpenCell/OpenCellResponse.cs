using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.Minesweeper.Commands.OpenCell
{
    public class OpenCellResponse
    {
        public GameStatus Status { get; set; }
        public List<OpenCellDto>? NewlyOpenedCells { get; set; }
    }
}
