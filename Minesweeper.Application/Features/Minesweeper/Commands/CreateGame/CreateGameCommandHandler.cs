using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;

namespace Minesweeper.Application.Features.Minesweeper.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IMinesweeperService _minesweeperService;

        public CreateGameCommandHandler(IMinesweeperService minesweeperService)
        {
            _minesweeperService = minesweeperService;
        }
        public Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = _minesweeperService.CreateNewGameAsync
                (request.Width, request.Height, request.TotalMines);

            return game;
        }
    }
}
