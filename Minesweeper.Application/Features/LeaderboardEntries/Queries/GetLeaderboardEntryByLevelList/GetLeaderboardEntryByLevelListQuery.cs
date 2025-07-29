using MediatR;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryByLevelList
{
    public class GetLeaderboardEntryByLevelListQuery : IRequest<List<LeaderboardEntryByLevelListVm>>
    {
        public GameLevel GameLevel { get; set; }
    }
}
