using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;

namespace Minesweeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinesweeperController(IMediator mediator) : Controller
    {
        [HttpPost(Name = "CreateGame")]
        public async Task<ActionResult<Guid>> CreateGame
            ([FromBody] CreateGameCommand createCategoryCommand)
        {
            var responce = await mediator.Send(createCategoryCommand);

            return Ok(responce);
        }
    }
}
