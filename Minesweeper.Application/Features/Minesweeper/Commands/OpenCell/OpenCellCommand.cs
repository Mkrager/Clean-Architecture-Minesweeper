﻿using MediatR;

namespace Minesweeper.Application.Features.Minesweeper.Commands.OpenCell
{
    public class OpenCellCommand : IRequest<OpenCellResponse>
    {
        public Guid GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
