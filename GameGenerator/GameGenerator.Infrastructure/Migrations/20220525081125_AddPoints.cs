using Microsoft.EntityFrameworkCore.Migrations;

namespace GameGenerator.Infrastructure.Migrations
{
    public partial class AddPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "UserEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "UserEntity");
        }
    }
}
