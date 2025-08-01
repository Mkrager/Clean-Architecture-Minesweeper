﻿using Minesweeper.Domain.Entities;

public class Game
{
    public Guid GameId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public GameStatus Status { get; set; }
    public Cell[,] Field { get; set; } = default!;
    public bool IsFirstMove { get; set; } = true;
    public int TotalMines { get; set; }
    public GameLevel GameLevel { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
