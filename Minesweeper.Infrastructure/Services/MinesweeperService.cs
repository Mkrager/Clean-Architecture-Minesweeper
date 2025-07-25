using Microsoft.Extensions.Caching.Memory;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Application.DTOs;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class MinesweeperService : IMinesweeperService
    {
        private readonly IMemoryCache _cache;
        private readonly IGameEngine _engine;
        private readonly MemoryCacheEntryOptions _cacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        public MinesweeperService(IMemoryCache cache, IGameEngine engine)
        {
            _cache = cache;
            _engine = engine;
        }

        public Task<Guid> CreateNewGameAsync(int width, int height, int mines)
        {
            var game = new Game()
            {
                Width = width,
                Height = height,
                TotalMines = mines
            };
            _engine.Initialize(game);
            var id = Guid.NewGuid();
            _cache.Set(id, game, _cacheOptions);
            return Task.FromResult(id);
        }

        public Task<GameStateDto> GetGameStateAsync(Guid gameId)
        {
            var game = GetGame(gameId);

            var cells = new List<CellDto>();
            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    var c = game.Field[x, y];
                    cells.Add(new CellDto
                    {
                        X = x,
                        Y = y,
                        IsOpened = c.IsOpened,
                        HasFlag = c.HasFlag,
                        HasMine = game.Status != GameStatus.InProgress && c.HasMine,
                        AdjacentMines = c.AdjacentMines
                    });
                }
            }

            var dto = new GameStateDto
            {
                GameId = gameId,
                Width = game.Width,
                Height = game.Height,
                Status = game.Status,
                Cells = cells
            };

            return Task.FromResult(dto);
        }

        public Task<OpenCellResult> OpenCellAsync(Guid gameId, int x, int y)
        {
            var game = GetGame(gameId);
            var result = _engine.OpenCell(game, x, y);

            var dto = new OpenCellResult
            {
                Status = game.Status,
                NewlyOpenedCells = result.Select(c => new CellDto
                {
                    X = c.X,
                    Y = c.Y,
                    IsOpened = c.cell.IsOpened,
                    HasFlag = c.cell.HasFlag,
                    HasMine = c.cell.HasMine,
                    AdjacentMines = c.cell.AdjacentMines
                }).ToList()
            };

            return Task.FromResult(dto);
        }

        public Task<ToggleFlagResult> ToggleFlagAsync(Guid gameId, int x, int y)
        {
            var game = GetGame(gameId);
            var cell = _engine.ToggleFlag(game, x, y);

            var dto = new ToggleFlagResult
            {
                Success = true,
                UpdatedCell = new CellDto
                {
                    X = x,
                    Y = y,
                    IsOpened = cell.IsOpened,
                    HasFlag = cell.HasFlag,
                    HasMine = cell.HasMine,
                    AdjacentMines = cell.AdjacentMines
                }
            };

            return Task.FromResult(dto);
        }

        private Game GetGame(Guid gameId)
        {
            if (!_cache.TryGetValue(gameId, out Game game))
                throw new Exception("Game noy found");
            return game;
        }
    }
}
