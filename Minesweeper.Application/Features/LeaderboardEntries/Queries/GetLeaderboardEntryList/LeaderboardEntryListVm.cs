namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList
{
    public class LeaderboardEntryListVm
    {
        public string PlayerName { get; set; } = string.Empty;
        public TimeSpan Time { get; set; }
    }
}
