using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Features.Solver.Command.Solve
{
    public class SolveCommandHandler : IRequestHandler<SolveCommand, SolveCommandResponse>
    {
        private readonly IMinesweeperSolverService _minesweeperSolver;
        private readonly IMinesweeperService _minesweeperService;
        private readonly IMapper _mapper;
        public SolveCommandHandler(IMinesweeperSolverService minesweeperSolver, IMinesweeperService minesweeperService, IMapper mapper)
        {
            _minesweeperSolver = minesweeperSolver;
            _minesweeperService = minesweeperService;
            _mapper = mapper;
        }
        public async Task<SolveCommandResponse> Handle(SolveCommand request, CancellationToken cancellationToken)
        {
            var game = _minesweeperService.GetGame(request.GameId);

            await _minesweeperService.OpenCellAsync(request.GameId, game.Width / 2, game.Height / 2);

            await _minesweeperSolver.SolveAsync(game);

            var result = await _minesweeperService.GetGameStateAsync(request.GameId);

            return _mapper.Map<SolveCommandResponse>(result);
        }
    }
}
