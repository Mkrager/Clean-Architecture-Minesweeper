using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.Exceptions;
using System.Runtime.CompilerServices;

namespace Minesweeper.Application.Features.Minesweeper.Queries.GetGameState
{
    public class GetGameStateQueryHandler : IRequestHandler<GetGameStateQuery, GameStateVm>
    {
        private readonly IMinesweeperService _minesweeperService;
        private readonly IMapper _mapper;
        public GetGameStateQueryHandler(IMinesweeperService minesweeperService, IMapper mapper)
        {
            _minesweeperService = minesweeperService;
            _mapper = mapper;
        }

        public async Task<GameStateVm> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
        {
            var state = await _minesweeperService.GetGameStateAsync(request.GameId);

            if (state == null)
                throw new NotFoundException(nameof(Game), request.GameId);

            return _mapper.Map<GameStateVm>(state);
        }
    }
}
