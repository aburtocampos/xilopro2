using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fix34535 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_AspNetUsers_AppUserId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_AppUserId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "AppUsersId",
                table: "Positions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AppUsersId",
                table: "Positions",
                column: "AppUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_AspNetUsers_AppUsersId",
                table: "Positions",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_AspNetUsers_AppUsersId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_AppUsersId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "AppUsersId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Positions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AppUserId",
                table: "Positions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_AspNetUsers_AppUserId",
                table: "Positions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
