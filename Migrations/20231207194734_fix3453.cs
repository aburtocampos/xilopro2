using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fix3453 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID");
        }
    }
}
