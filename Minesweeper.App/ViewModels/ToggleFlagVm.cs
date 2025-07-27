namespace Minesweeper.App.ViewModels
{
    public class ToggleFlagVm
    {
        public bool Success { get; set; }
        public GameStateCellDto UpdatedCell { get; set; } = default!;
    }
}
