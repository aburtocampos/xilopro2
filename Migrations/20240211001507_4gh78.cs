using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class _4gh78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_VisitorTeam_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_VisitorTeam_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "VisitorTeam_ID",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId",
                table: "Matches",
                column: "TeamLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamVisitorId",
                table: "Matches",
                column: "TeamVisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches",
                column: "TeamLocalId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches",
                column: "TeamVisitorId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamVisitorId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "LocalTeam_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VisitorTeam_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LocalTeam_ID",
                table: "Matches",
                column: "LocalTeam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_VisitorTeam_ID",
                table: "Matches",
                column: "VisitorTeam_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_LocalTeam_ID",
                table: "Matches",
                column: "LocalTeam_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_VisitorTeam_ID",
                table: "Matches",
                column: "VisitorTeam_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
