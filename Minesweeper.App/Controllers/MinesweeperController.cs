using Microsoft.AspNetCore.Mvc;
using Minesweeper.App.Contracts;

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


            return View();
        }
    }
}
