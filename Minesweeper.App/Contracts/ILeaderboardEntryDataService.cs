using Minesweeper.App.ViewModels;

namespace Minesweeper.App.Contracts
{
    public interface ILeaderboardEntryDataService
    {
        Task<Guid> CreateLeaderboardEntry(LeaderboardViewModel leaderboardVm);
        Task<List<LeaderbordListViewModel>> GetLeaderbordList();
        Task<List<LeaderbordListViewModel>> GetLeaderbordListByLevel(GameLevel gameLevel);
    }
}
