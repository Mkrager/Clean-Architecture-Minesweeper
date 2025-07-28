using Microsoft.AspNetCore.Mvc;
using Minesweeper.App.Contracts;
using Minesweeper.App.ViewModels;

namespace Minesweeper.App.Controllers
{
    public class LeaderboardEntryController : Controller
    {
        private readonly ILeaderboardEntryDataService _leaderboardEntryDataService;

        public LeaderboardEntryController(ILeaderboardEntryDataService leaderboardEntryDataService)
        {
            _leaderboardEntryDataService = leaderboardEntryDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaderboardViewModel leaderboardViewModel)
        {
            var result = await _leaderboardEntryDataService.CreateLeaderboardEntry(leaderboardViewModel);

            return Ok();
        }
    }
}
