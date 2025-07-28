using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minesweeper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixcolumnbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievedAt1",
                table: "LeaderboardEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AchievedAt1",
                table: "LeaderboardEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
