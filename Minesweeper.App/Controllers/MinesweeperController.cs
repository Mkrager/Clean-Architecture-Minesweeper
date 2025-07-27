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

        public async Task<IActionResult> Start()  
        {
            var game = await _minesweeperService.CreateSmallGame();

            var gameState = await _minesweeperService.GetGameState(game);

            return View(gameState);
        }

        public async Task<IActionResult> Continue(Guid gameId)
        {
            var gameState = await _minesweeperService.GetGameState(gameId);

            return View(gameState);
        }
        [HttpPost]
        public async Task<IActionResult> OpenCell(Guid gameId, int x, int y)
        {
            var response = await _minesweeperService.OpenCell(new OpenCellRequest()
            {
                GameId = gameId,
                X = x,
                Y = y
            });

            var gameState = await _minesweeperService.GetGameState(gameId);

            return RedirectToAction("Continue", "Minesweeper", new { gameId = gameId});
        }
    }
}
