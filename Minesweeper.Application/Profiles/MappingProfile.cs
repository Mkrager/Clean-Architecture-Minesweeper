using AutoMapper;
using Minesweeper.Application.DTOs;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;

namespace Minesweeper.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameStateDto, GameStateVm>().ReverseMap();
            CreateMap<GameStateCellDto, CellDto>().ReverseMap();

            CreateMap<OpenCellResult, OpenCellResponse>().ReverseMap();
            CreateMap<CellDto, OpenCellDto>().ReverseMap();
        }
    }
}
