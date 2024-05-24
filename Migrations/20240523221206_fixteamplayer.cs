using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fixteamplayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Teamid",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamid_Player_Dorsal",
                table: "Players",
                columns: new[] { "Teamid", "Player_Dorsal" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Teamid_Player_Dorsal",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamid",
                table: "Players",
                column: "Teamid");
        }
    }
}
