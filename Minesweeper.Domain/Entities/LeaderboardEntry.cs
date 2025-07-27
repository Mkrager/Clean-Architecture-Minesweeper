namespace Minesweeper.Domain.Entities
{
    public class LeaderboardEntry
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public TimeSpan BestTime { get; set; }
        public DateTime AchievedAt { get; set; }
    }

}
