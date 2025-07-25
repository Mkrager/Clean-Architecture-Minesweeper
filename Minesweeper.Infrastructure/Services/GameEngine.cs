using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Domain.Entities;

namespace Minesweeper.Infrastructure.Services
{
    public class GameEngine : IGameEngine
    {
        public void Initialize(Game game)
        {
            game.Status = GameStatus.InProgress;
            game.Field = new Cell[game.Width, game.Height];
            for (int x = 0; x < game.Width; x++)
                for (int y = 0; y < game.Height; y++)
                    game.Field[x, y] = new Cell();
        }

        public List<(int X, int Y, Cell cell)> OpenCell(Game game, int x, int y)
        {
            if (game.Status != GameStatus.InProgress)
                return new();

            if (game.IsFirstMove)
            {
                GenerateMines(game, x, y);
                game.IsFirstMove = false;
            }

            var opened = new List<(int X, int Y, Cell cell)>();
            Reveal(game, x, y, opened);

            if (game.Field[x, y].HasMine)
                game.Status = GameStatus.Lost;
            else if (IsGameWon(game))
                game.Status = GameStatus.Won;

            return opened;
        }

        public Cell ToggleFlag(Game game, int x, int y)
        {
            var cell = game.Field[x, y];
            if (game.Status != GameStatus.InProgress || cell.IsOpened)
                return cell;

            cell.HasFlag = !cell.HasFlag;
            return cell;
        }

        private void Reveal(Game game, int x, int y, List<(int X, int Y, Cell cell)> opened)
        {
            if (x < 0 || y < 0 || x >= game.Width || y >= game.Height)
                return;

            var cell = game.Field[x, y];
            if (cell.IsOpened || cell.HasFlag)
                return;

            cell.IsOpened = true;
            opened.Add((x, y, cell));

            if (cell.AdjacentMines == 0 && !cell.HasMine)
            {
                foreach (var (dx, dy) in GetNeighbors())
                    Reveal(game, x + dx, y + dy, opened);
            }
        }

        private void GenerateMines(Game game, int excludeX, int excludeY)
        {
            var rand = new Random();
            int placed = 0;

            while (placed < game.TotalMines)
            {
                int x = rand.Next(game.Width);
                int y = rand.Next(game.Height);

                if ((x == excludeX && y == excludeY) || game.Field[x, y].HasMine)
                    continue;

                game.Field[x, y].HasMine = true;
                placed++;
            }

            for (int x = 0; x < game.Width; x++)
                for (int y = 0; y < game.Height; y++)
                    game.Field[x, y].AdjacentMines = CountAdjacentMines(game, x, y);
        }

        private int CountAdjacentMines(Game game, int x, int y)
        {
            int count = 0;
            foreach (var (dx, dy) in GetNeighbors())
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && ny >= 0 && nx < game.Width && ny < game.Height && game.Field[nx, ny].HasMine)
                    count++;
            }
            return count;
        }

        private IEnumerable<(int dx, int dy)> GetNeighbors()
        {
            return new (int, int)[]
            {
                (-1, -1), (0, -1), (1, -1),
                (-1,  0),          (1,  0),
                (-1,  1), (0,  1), (1,  1)
            };
        }

        private bool IsGameWon(Game game)
        {
            for (int x = 0; x < game.Width; x++)
                for (int y = 0; y < game.Height; y++)
                    if (!game.Field[x, y].IsOpened && !game.Field[x, y].HasMine)
                        return false;
            return true;
        }
    }
}
