using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;

namespace Minesweeper.Application.Features.Minesweeper.Commands.OpenCell
{
    public class OpenCellCommandHandler : IRequestHandler<OpenCellCommand, OpenCellResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMinesweeperService _minesweeperService;
        private readonly INotificationService _notificationService;
        public OpenCellCommandHandler(IMapper mapper, IMinesweeperService minesweeperService, INotificationService notificationService)
        {
            _mapper = mapper;
            _minesweeperService = minesweeperService;
            _notificationService = notificationService;
        }
        public async Task<OpenCellResponse> Handle(OpenCellCommand request, CancellationToken cancellationToken)
        {
            var result = await _minesweeperService.OpenCellAsync
                (request.GameId, request.X, request.Y);

            await _notificationService.NotifyAsync(request.GameId, result);

            return _mapper.Map<OpenCellResponse>(result);
        }
    }
}
