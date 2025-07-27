namespace Minesweeper.App.ViewModels
{
    public class CreateGameRequest
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int TotalMines { get; set; }
    }
}
