using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneoonmatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "torneoid",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_torneoid",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "Jornada", "torneoid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada_torneoid",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "torneoid",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_Jornada",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "Jornada" },
                unique: true);
        }
    }
}
