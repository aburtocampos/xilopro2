using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_MatchId_PlayerId",
                table: "PlayerStatistics",
                columns: new[] { "MatchId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_MatchId_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId",
                unique: true);
        }
    }
}
