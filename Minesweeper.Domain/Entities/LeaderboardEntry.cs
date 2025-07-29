using Minesweeper.Domain.Common;

namespace Minesweeper.Domain.Entities
{
    public class LeaderboardEntry : CreatableEntity
    {
        public string PlayerName { get; set; } = string.Empty;
        public GameLevel GameLevel { get; set; }
        public TimeSpan Time { get; set; }
    }
}
