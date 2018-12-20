using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class inage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");
        }
    }
}
