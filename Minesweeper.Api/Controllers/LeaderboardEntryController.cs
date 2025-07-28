using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry;

namespace Minesweeper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardEntryController(IMediator mediator) : Controller
    {
        [HttpPost(Name = "CreateLeaderboardEntry")]
        public async Task<ActionResult<Guid>> CreateLeaderboardEntry([FromBody] CreateLeaderboadEntryCommand createLeaderboadEntryCommand)
        {
            var responce = await mediator.Send(createLeaderboadEntryCommand);

            return Ok(responce);
        }
    }
}
