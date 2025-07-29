namespace Minesweeper.App.ViewModels
{
    public class GameStateViewModel
    {
        public Guid GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GameStatus Status { get; set; }
        public int TotalMines { get; set; }
        public List<GameStateCellDto> Cells { get; set; } = new();
    }
}
