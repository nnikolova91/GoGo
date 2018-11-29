using Microsoft.EntityFrameworkCore.Migrations;

namespace GoGo.Data.Migrations
{
    public partial class delBaseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Destinations_DestinationId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_GoUserPhoto_UserId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_DestinationId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_UserId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_GoUserPhoto_UserId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "GoUserPhoto_UserId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "GoUserPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoUserPhoto",
                table: "GoUserPhoto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DestinationPhoto",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DestinationId = table.Column<string>(nullable: true),
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
                name: "IX_GoUserPhoto_UserId",
                table: "GoUserPhoto",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationPhoto_DestinationId",
                table: "DestinationPhoto",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationPhoto_UserId",
                table: "DestinationPhoto",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoUserPhoto_AspNetUsers_UserId",
                table: "GoUserPhoto",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoUserPhoto_AspNetUsers_UserId",
                table: "GoUserPhoto");

            migrationBuilder.DropTable(
                name: "DestinationPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoUserPhoto",
                table: "GoUserPhoto");

            migrationBuilder.DropIndex(
                name: "IX_GoUserPhoto_UserId",
                table: "GoUserPhoto");

            migrationBuilder.RenameTable(
                name: "GoUserPhoto",
                newName: "Photo");

            migrationBuilder.AddColumn<string>(
                name: "DestinationId",
                table: "Photo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoUserPhoto_UserId",
                table: "Photo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Photo",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_DestinationId",
                table: "Photo",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_GoUserPhoto_UserId",
                table: "Photo",
                column: "GoUserPhoto_UserId",
                unique: true,
                filter: "[GoUserPhoto_UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Destinations_DestinationId",
                table: "Photo",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_GoUserPhoto_UserId",
                table: "Photo",
                column: "GoUserPhoto_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
