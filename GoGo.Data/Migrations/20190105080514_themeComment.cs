using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class themeComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemComments_AspNetUsers_AuthorId",
                table: "ThemComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ThemComments_Thems_ThemId",
                table: "ThemComments");

            migrationBuilder.DropTable(
                name: "Thems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemComments",
                table: "ThemComments");

            migrationBuilder.RenameTable(
                name: "ThemComments",
                newName: "ThemeComments");

            migrationBuilder.RenameIndex(
                name: "IX_ThemComments_ThemId",
                table: "ThemeComments",
                newName: "IX_ThemeComments_ThemId");

            migrationBuilder.RenameIndex(
                name: "IX_ThemComments_AuthorId",
                table: "ThemeComments",
                newName: "IX_ThemeComments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemeComments",
                table: "ThemeComments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_AuthorId",
                table: "Themes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeComments_AspNetUsers_AuthorId",
                table: "ThemeComments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeComments_Themes_ThemId",
                table: "ThemeComments",
                column: "ThemId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeComments_AspNetUsers_AuthorId",
                table: "ThemeComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ThemeComments_Themes_ThemId",
                table: "ThemeComments");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemeComments",
                table: "ThemeComments");

            migrationBuilder.RenameTable(
                name: "ThemeComments",
                newName: "ThemComments");

            migrationBuilder.RenameIndex(
                name: "IX_ThemeComments_ThemId",
                table: "ThemComments",
                newName: "IX_ThemComments_ThemId");

            migrationBuilder.RenameIndex(
                name: "IX_ThemeComments_AuthorId",
                table: "ThemComments",
                newName: "IX_ThemComments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemComments",
                table: "ThemComments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Thems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thems_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thems_AuthorId",
                table: "Thems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThemComments_AspNetUsers_AuthorId",
                table: "ThemComments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemComments_Thems_ThemId",
                table: "ThemComments",
                column: "ThemId",
                principalTable: "Thems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
