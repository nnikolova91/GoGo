using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class @in : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamLevelGames");

            migrationBuilder.CreateTable(
                name: "LevelsParticipants",
                columns: table => new
                {
                    ParticipantId = table.Column<string>(nullable: false),
                    LevelId = table.Column<string>(nullable: false),
                    GameId = table.Column<string>(nullable: false),
                    CorrespondingImage = table.Column<byte[]>(nullable: true),
                    IsPassed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsParticipants", x => new { x.ParticipantId, x.GameId, x.LevelId });
                    table.ForeignKey(
                        name: "FK_LevelsParticipants_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelsParticipants_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelsParticipants_AspNetUsers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelsParticipants_GameId",
                table: "LevelsParticipants",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelsParticipants_LevelId",
                table: "LevelsParticipants",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelsParticipants");

            migrationBuilder.CreateTable(
                name: "TeamLevelGames",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    GameId = table.Column<string>(nullable: false),
                    LevelId = table.Column<string>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    IsPassed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLevelGames", x => new { x.TeamId, x.GameId, x.LevelId });
                    table.ForeignKey(
                        name: "FK_TeamLevelGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamLevelGames_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamLevelGames_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamLevelGames_GameId",
                table: "TeamLevelGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLevelGames_LevelId",
                table: "TeamLevelGames",
                column: "LevelId");
        }
    }
}
