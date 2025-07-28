using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minesweeper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityAndChangeColumnNameFromBestTimeToTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BestTime",
                table: "LeaderboardEntries",
                newName: "Time");

            migrationBuilder.AddColumn<DateTime>(
                name: "AchievedAt1",
                table: "LeaderboardEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievedAt1",
                table: "LeaderboardEntries");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "LeaderboardEntries",
                newName: "BestTime");
        }
    }
}
