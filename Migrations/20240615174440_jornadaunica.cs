using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class jornadaunica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamVisitorId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamVisitorId_TeamLocalId_Jornada_GroupsrId",
                table: "Matches",
                columns: new[] { "TeamVisitorId", "TeamLocalId", "Jornada", "GroupsrId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamVisitorId_TeamLocalId_Jornada_GroupsrId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamVisitorId",
                table: "Matches",
                column: "TeamVisitorId");
        }
    }
}
