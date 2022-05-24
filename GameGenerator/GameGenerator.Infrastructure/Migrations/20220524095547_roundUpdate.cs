using Microsoft.EntityFrameworkCore.Migrations;

namespace GameGenerator.Infrastructure.Migrations
{
    public partial class roundUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentRound",
                table: "OnGoingGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRound",
                table: "OnGoingGames");
        }
    }
}
