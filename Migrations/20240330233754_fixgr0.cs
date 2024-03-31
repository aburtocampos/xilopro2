using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fixgr0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Matches_MatchgamesMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_MatchgamesMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "MatchgamesMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.AddColumn<int>(
                name: "MatchgameMatch_ID",
                table: "PlayerStatistics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_MatchgameMatch_ID",
                table: "PlayerStatistics",
                column: "MatchgameMatch_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Matches_MatchgameMatch_ID",
                table: "PlayerStatistics",
                column: "MatchgameMatch_ID",
                principalTable: "Matches",
                principalColumn: "Match_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Matches_MatchgameMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_MatchgameMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "MatchgameMatch_ID",
                table: "PlayerStatistics");

            migrationBuilder.AddColumn<int>(
                name: "MatchgamesMatch_ID",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_MatchgamesMatch_ID",
                table: "PlayerStatistics",
                column: "MatchgamesMatch_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Matches_MatchgamesMatch_ID",
                table: "PlayerStatistics",
                column: "MatchgamesMatch_ID",
                principalTable: "Matches",
                principalColumn: "Match_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
