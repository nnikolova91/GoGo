using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class removePhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationPhoto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DestinationPhoto",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DestinationId = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationPhoto_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DestinationPhoto_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinationPhoto_DestinationId",
                table: "DestinationPhoto",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationPhoto_UserId",
                table: "DestinationPhoto",
                column: "UserId");
        }
    }
}
