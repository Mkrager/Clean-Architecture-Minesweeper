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

        [HttpGet]
        public async Task<IActionResult> ListByLevel(string gameLevel)
        {
            if (!Enum.TryParse<GameLevel>(gameLevel, ignoreCase: true, out var level))
            {
                return BadRequest("Invalid game level.");
            }

            var list = await _leaderboardEntryDataService.GetLeaderbordListByLevel(level);
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _leaderboardEntryDataService.GetLeaderbordList();

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaderboardViewModel leaderboardViewModel)
        {
            var result = await _leaderboardEntryDataService.CreateLeaderboardEntry(leaderboardViewModel);

            return Ok();
        }
    }
}
