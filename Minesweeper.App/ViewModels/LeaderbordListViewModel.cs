namespace Minesweeper.App.ViewModels
{
    public class LeaderbordListViewModel
    {
        public string PlayerName { get; set; } = string.Empty;
        public GameLevel GameLevel { get; set; }
        public TimeSpan Time { get; set; }
    }
}
