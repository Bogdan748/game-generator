using Microsoft.EntityFrameworkCore.Migrations;

namespace GameGenerator.Infrastructure.Migrations
{
    public partial class OnGoingTablesV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OnGoingGameId",
                table: "UserEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OnGoingGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameGroup = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnGoingGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnGoingCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    OnGoingGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnGoingCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnGoing_Card",
                        column: x => x.CardId,
                        principalTable: "CardEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnGoing_Game",
                        column: x => x.OnGoingGameId,
                        principalTable: "OnGoingGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnGoing_User",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_OnGoingGameId",
                table: "UserEntity",
                column: "OnGoingGameId");

            migrationBuilder.CreateIndex(
                name: "IX_OnGoingCards_CardId",
                table: "OnGoingCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_OnGoingCards_OnGoingGameId",
                table: "OnGoingCards",
                column: "OnGoingGameId");

            migrationBuilder.CreateIndex(
                name: "IX_OnGoingCards_UserId",
                table: "OnGoingCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_OnGoingGame",
                table: "UserEntity",
                column: "OnGoingGameId",
                principalTable: "OnGoingGames",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_OnGoingGame",
                table: "UserEntity");

            migrationBuilder.DropTable(
                name: "OnGoingCards");

            migrationBuilder.DropTable(
                name: "OnGoingGames");

            migrationBuilder.DropIndex(
                name: "IX_UserEntity_OnGoingGameId",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "OnGoingGameId",
                table: "UserEntity");
        }
    }
}
