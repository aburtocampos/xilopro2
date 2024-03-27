using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class ty002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_AspNetUsers_UserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Player_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Matches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Player_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player_ID",
                table: "Matches",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_AspNetUsers_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player_ID",
                table: "Matches",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
