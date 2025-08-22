using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Minesweeper.Solver
{
    public class MinesweeperSolver : IMinesweeperSolverService
    {
        private readonly IMinesweeperService _minesweeperService;
        private readonly IEnumerable<IMoveStrategy> _strategies;

        public MinesweeperSolver(IMinesweeperService minesweeperService, IEnumerable<IMoveStrategy> strategies)
        {
            _minesweeperService = minesweeperService;
            _strategies = strategies;
        }

        public async Task SolveAsync(Game game)
        {
            while (game.Status == GameStatus.InProgress)
            {
                bool moveMade = false;

                foreach (var strategy in _strategies)
                {
                    if (await strategy.ApplyAsync(game, _minesweeperService))
                        moveMade = true;
                }

                if (!moveMade) break;
            }
        }
    }
}