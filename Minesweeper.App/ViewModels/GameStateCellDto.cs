namespace Minesweeper.App.ViewModels
{
    public class GameStateCellDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasMine { get; set; }
        public bool IsOpened { get; set; }
        public bool HasFlag { get; set; }
        public int AdjacentMines { get; set; }
    }
}
