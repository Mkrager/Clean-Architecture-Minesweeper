using Minesweeper.Domain.Entities;

public class Game
{
    public int Width { get; set; }
    public int Height { get; set; }
    public GameStatus Status { get; set; }
    public Cell[,] Field { get; set; } = default!;
    public bool IsFirstMove { get; set; } = true;
    public int TotalMines { get; set; }
}
