using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Application.Features.Solver.Command.Solve;

namespace Minesweeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolverController(IMediator mediator) : Controller
    {
        [HttpPost(Name = "SolveGame")]
        public async Task<ActionResult<SolveCommandResponse>> CreateGame(Guid gameId)
        {
            var responce = await mediator.Send(new SolveCommand()
            {
                GameId = gameId
            });

            return Ok(responce);
        }

    }
}
