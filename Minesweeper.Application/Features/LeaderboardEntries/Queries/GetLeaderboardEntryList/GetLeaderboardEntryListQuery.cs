using MediatR;

namespace Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList
{
    public class GetLeaderboardEntryListQuery : IRequest<List<LeaderboardEntryListVm>>
    {
    }
}
