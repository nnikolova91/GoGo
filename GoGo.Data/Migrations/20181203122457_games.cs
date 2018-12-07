using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class games : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Teams_TeamId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Game_GameId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamLevelGames_Game_GameId",
                table: "TeamLevelGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Game_TeamId",
                table: "Games",
                newName: "IX_Games_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Games_GameId",
                table: "Levels",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLevelGames_Games_GameId",
                table: "TeamLevelGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Games_GameId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamLevelGames_Games_GameId",
                table: "TeamLevelGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Games_TeamId",
                table: "Game",
                newName: "IX_Game_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Teams_TeamId",
                table: "Game",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Game_GameId",
                table: "Levels",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLevelGames_Game_GameId",
                table: "TeamLevelGames",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
