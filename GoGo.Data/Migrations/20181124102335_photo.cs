using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "GoUserPhoto");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "GoUserPhoto",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "DestinationPhoto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "GoUserPhoto");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "DestinationPhoto");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "GoUserPhoto",
                nullable: true);
        }
    }
}
