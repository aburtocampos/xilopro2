using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class hg004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Player_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
