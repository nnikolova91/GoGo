using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class initialCources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cources",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxCountParticipants = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    DurationOfDays = table.Column<int>(nullable: false),
                    CountOfHours = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourcesUsers",
                columns: table => new
                {
                    CourceId = table.Column<string>(nullable: false),
                    ParticipantId = table.Column<string>(nullable: false),
                    StatusUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourcesUsers", x => new { x.CourceId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_CourcesUsers_Cources_CourceId",
                        column: x => x.CourceId,
                        principalTable: "Cources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourcesUsers_AspNetUsers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourcesUsers_ParticipantId",
                table: "CourcesUsers",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourcesUsers");

            migrationBuilder.DropTable(
                name: "Cources");
        }
    }
}
