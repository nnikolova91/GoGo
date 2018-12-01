using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class creator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Cources",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cources_CreatorId",
                table: "Cources",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cources_AspNetUsers_CreatorId",
                table: "Cources",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cources_AspNetUsers_CreatorId",
                table: "Cources");

            migrationBuilder.DropIndex(
                name: "IX_Cources_CreatorId",
                table: "Cources");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Cources");
        }
    }
}
