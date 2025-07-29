using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minesweeper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGameLevelToLeaderboardEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameLevel",
                table: "LeaderboardEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameLevel",
                table: "LeaderboardEntries");
        }
    }
}
