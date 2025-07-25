using AutoMapper;
using Minesweeper.Application.DTOs;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameStateDto, GameStateVm>().ReverseMap();
            CreateMap<GameStateCellDto, CellDto>().ReverseMap();
        }
    }
}
