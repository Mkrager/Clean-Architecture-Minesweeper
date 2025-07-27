using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;

namespace Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag
{
    public class ToggleFlagCommandHandler : IRequestHandler<ToggleFlagCommand, ToggleFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMinesweeperService _minesweeperservice;
        private readonly INotificationService _notificationService;
        public ToggleFlagCommandHandler(
            IMapper mapper, 
            IMinesweeperService minesweeperservice, 
            INotificationService notificationService)
        {
            _mapper = mapper;
            _minesweeperservice = minesweeperservice;
            _notificationService = notificationService;
        }
        public async Task<ToggleFlagResponse> Handle(ToggleFlagCommand request, CancellationToken cancellationToken)
        {
            var result = await _minesweeperservice.ToggleFlagAsync
                (request.GameId, request.X, request.Y);

            await _notificationService.NotifyAsync(result);

            return _mapper.Map<ToggleFlagResponse>(result);
        }
    }
}
