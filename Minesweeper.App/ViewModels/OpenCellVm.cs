namespace Minesweeper.App.ViewModels
{
    public class OpenCellVm
    {
        public GameStatus Status { get; set; }
        public List<GameStateCellDto>? NewlyOpenedCells { get; set; }
    }
}
