using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Application.Features.Minesweeper.Commands.CreateGame;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;

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

        [HttpGet("{id}", Name = "GetGameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameStateVm>> GetCourseById(Guid id)
        {
            var getGameStateQuery = new GetGameStateQuery() { GameId = id };
            return Ok(await mediator.Send(getGameStateQuery));
        }

    }
}
