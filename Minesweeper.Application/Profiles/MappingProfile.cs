using AutoMapper;
using Minesweeper.Application.DTOs;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryByLevelList;
using Minesweeper.Application.Features.LeaderboardEntries.Queries.GetLeaderboardEntryList;
using Minesweeper.Application.Features.Minesweeper.Commands.OpenCell;
using Minesweeper.Application.Features.Minesweeper.Commands.ToggleFlag;
using Minesweeper.Application.Features.Minesweeper.Queries.GetGameState;
using Minesweeper.Application.Features.Solver.Command.Solve;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameStateDto, GameStateVm>().ReverseMap();
            CreateMap<GameStateDto, SolveCommandResponse>().ReverseMap();
    
            CreateMap<GameStateCellDto, CellDto>().ReverseMap();

            CreateMap<OpenCellResult, OpenCellResponse>().ReverseMap();
            CreateMap<CellDto, OpenCellDto>().ReverseMap();

            CreateMap<ToggleFlagResult, ToggleFlagResponse>().ReverseMap();
            CreateMap<CellDto, ToggleFlagCellDto>().ReverseMap();

            CreateMap<LeaderboardEntry, LeaderboardEntryListVm>().ReverseMap();
            CreateMap<LeaderboardEntry, LeaderboardEntryByLevelListVm>().ReverseMap();
        }
    }
}
