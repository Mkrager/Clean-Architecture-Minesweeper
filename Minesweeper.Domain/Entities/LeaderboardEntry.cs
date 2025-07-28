using Minesweeper.Domain.Common;

namespace Minesweeper.Domain.Entities
{
    public class LeaderboardEntry : CreatableEntity
    {
        public string PlayerName { get; set; } = string.Empty;
        public TimeSpan Time { get; set; }
        public DateTime AchievedAt { get; set; }
    }

}
