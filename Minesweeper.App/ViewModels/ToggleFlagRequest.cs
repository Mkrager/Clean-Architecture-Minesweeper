namespace Minesweeper.App.ViewModels
{
    public class ToggleFlagRequest
    {
        public Guid GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
