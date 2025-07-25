namespace Minesweeper.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GameStatus Status { get; set; }
        public Cell[,] Field { get; set; } = default!;
        public int TotalMines { get; set; }
        public bool IsFirstMove { get; set; } = true;
    }
}
