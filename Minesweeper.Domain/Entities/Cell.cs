namespace Minesweeper.Domain.Entities
{
    public class Cell
    {
        public bool HasMine { get; set; }
        public bool IsOpened { get; set; }
        public bool HasFlag { get; set; }
        public int AdjacentMines { get; set; }
    }
}
