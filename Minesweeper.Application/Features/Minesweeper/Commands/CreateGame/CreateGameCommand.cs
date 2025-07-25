using MediatR;

namespace Minesweeper.Application.Features.Minesweeper.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<Guid>
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TotalMines { get; set; }
    }
}
