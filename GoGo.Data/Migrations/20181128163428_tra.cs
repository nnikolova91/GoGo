using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class tra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoUserPhoto");

            migrationBuilder.AddColumn<int>(
                name: "Socialization",
                table: "DestinationsUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Socialization",
                table: "DestinationsUsers");

            migrationBuilder.CreateTable(
                name: "GoUserPhoto",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoUserPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoUserPhoto_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoUserPhoto_UserId",
                table: "GoUserPhoto",
                column: "UserId");
        }
    }
}
