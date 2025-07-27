namespace Minesweeper.App.ViewModels
{
    public class OpenCellRequest
    {
        public Guid GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
