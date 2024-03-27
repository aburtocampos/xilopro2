using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class ty004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "Jornada",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_Jornada",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "Jornada",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId",
                table: "Matches",
                column: "TeamLocalId");
        }
    }
}
