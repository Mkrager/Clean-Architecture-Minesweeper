using AutoMapper;
using MediatR;
using Minesweeper.Application.Contracts.Infrastructure;

namespace Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag
{
    public class ToggleFlagCommandHandler : IRequestHandler<ToggleFlagCommand, ToggleFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMinesweeperService _minesweeperservice;

        public ToggleFlagCommandHandler(IMapper mapper, IMinesweeperService minesweeperservice)
        {
            _mapper = mapper;
            _minesweeperservice = minesweeperservice;
        }
        public async Task<ToggleFlagResponse> Handle(ToggleFlagCommand request, CancellationToken cancellationToken)
        {
            var result = await _minesweeperservice.ToggleFlagAsync
                (request.GameId, request.X, request.Y);

            return _mapper.Map<ToggleFlagResponse>(result);
        }
    }
}
