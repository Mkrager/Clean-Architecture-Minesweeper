using Microsoft.AspNetCore.Mvc;

namespace Minesweeper.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
