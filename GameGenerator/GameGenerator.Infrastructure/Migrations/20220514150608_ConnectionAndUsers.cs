using Microsoft.EntityFrameworkCore.Migrations;

namespace GameGenerator.Infrastructure.Migrations
{
    public partial class ConnectionAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionEntity",
                columns: table => new
                {
                    ConnectionID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Connected = table.Column<bool>(type: "bit", nullable: false),
                    UserEntityUserName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionEntity", x => x.ConnectionID);
                    table.ForeignKey(
                        name: "FK_Connection_User",
                        column: x => x.UserEntityUserName,
                        principalTable: "UserEntity",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionEntity_UserEntityUserName",
                table: "ConnectionEntity",
                column: "UserEntityUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
