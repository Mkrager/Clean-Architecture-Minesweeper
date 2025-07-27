using Microsoft.AspNetCore.Mvc;
using Minesweeper.App.Contracts;
using Minesweeper.App.ViewModels;

namespace Minesweeper.App.Controllers
{
    public class MinesweeperController : Controller
    {
        private readonly IMinesweeperService _minesweeperService;
        public MinesweeperController(IMinesweeperService minesweeperService)
        {
            _minesweeperService = minesweeperService;
        }

        [HttpGet]
        public async Task<IActionResult> Game()  
        {
            var game = await _minesweeperService.CreateSmallGame();

            var gameState = await _minesweeperService.GetGameState(game);

            return View(gameState);
        }

        [HttpGet]
        public async Task<IActionResult> GetGameState(Guid gameId)
        {
            var result = await _minesweeperService.GetGameState(gameId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> OpenCell([FromBody] OpenCellRequest openCellRequest)
        {
            var result = await _minesweeperService.OpenCell(openCellRequest);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ToggleFlag([FromBody] ToggleFlagRequest toggleFlagRequest)
        {
            var res = await _minesweeperService.ToggleFlag(toggleFlagRequest);

            return Ok();
        }

    }
}
