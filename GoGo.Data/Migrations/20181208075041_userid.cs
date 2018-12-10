using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Levels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Levels");
        }
    }
}
