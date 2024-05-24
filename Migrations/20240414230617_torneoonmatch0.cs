using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneoonmatch0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_torneoid",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_GroupsrId",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "Jornada", "GroupsrId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_GroupsrId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_torneoid",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "Jornada", "torneoid" },
                unique: true);
        }
    }
}
