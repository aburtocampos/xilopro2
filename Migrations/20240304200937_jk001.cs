using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class jk001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_Jornada",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "Jornada" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Jornada",
                table: "Matches",
                column: "Jornada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId" },
                unique: true);
        }
    }
}
