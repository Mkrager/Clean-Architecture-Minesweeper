namespace Minesweeper.Application.DTOs
{
    public class ToggleFlagResult
    {
        public bool Success { get; set; }
        public CellDto UpdatedCell { get; set; } = default!;
    }
}
