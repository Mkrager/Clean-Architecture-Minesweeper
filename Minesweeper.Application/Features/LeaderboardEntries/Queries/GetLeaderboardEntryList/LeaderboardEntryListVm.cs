using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList
{
    public class LeaderboardEntryListVm
    {
        public string PlayerName { get; set; } = string.Empty;
        public TimeSpan Time { get; set; }
        public GameLevel GameLevel { get; set; }
    }
}
