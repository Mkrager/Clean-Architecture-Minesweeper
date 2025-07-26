namespace Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag
{
    public class ToggleFlagResponse
    {
        public bool Success { get; set; }
        public ToggleFlagCellDto UpdatedCell { get; set; } = default!;
    }
}
