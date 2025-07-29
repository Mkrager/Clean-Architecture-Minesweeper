using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Application.Features.LeaderboardEntries.Commands.CreateLeaderboadEntry;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryByLevelList;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList;
using Minesweeper.Domain.Entities;

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

        [HttpGet(Name = "GetLeaderboard")]
        public async Task<ActionResult<List<LeaderboardEntryListVm>>> GetLeaderboard()
        {
            var responce = await mediator.Send(new GetLeaderboardEntryListQuery());

            return Ok(responce);
        }

        [HttpGet("{gameLevel}", Name = "GetLeaderboardByLevel")]
        public async Task<ActionResult<List<LeaderboardEntryByLevelListVm>>> GetLeaderboardByLevel(GameLevel gameLevel)
        {
            var responce = await mediator.Send(new GetLeaderboardEntryByLevelListQuery()
            {
                GameLevel = gameLevel
            });

            return Ok(responce);
        }
    }
}
