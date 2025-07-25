namespace Minesweeper.Application.Features.Minesweeper.Queries.GetGameState
{
    public class GameStateCellDto
    {
        public bool HasMine { get; set; }
        public bool IsOpened { get; set; }
        public bool HasFlag { get; set; }
        public int AdjacentMines { get; set; }
    }
}
