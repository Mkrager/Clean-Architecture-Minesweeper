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

        [HttpPost]
        public async Task<IActionResult> Game(CreateGameRequest createGameRequest)  
        {
            var game = await _minesweeperService.CreateGame(createGameRequest);

            var gameState = await _minesweeperService.GetGameState(game);

            gameState.TotalMines = createGameRequest.TotalMines;

            return View(gameState);
        }
    }
}
