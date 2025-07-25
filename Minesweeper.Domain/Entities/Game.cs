using Minesweeper.Domain.Entities;

public class Game
{
    public int Width { get; }
    public int Height { get; }
    public GameStatus Status { get; set; }
    public Cell[,] Field { get; set; } = default!;
    public bool IsFirstMove { get; set; } = true;
    public int TotalMines { get; }

    public Game(int width, int height, int totalMines)
    {
        Width = width; 
        Height = height; 
        TotalMines = totalMines;
    }
}
