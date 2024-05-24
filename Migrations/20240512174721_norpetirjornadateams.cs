using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class norpetirjornadateams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_GroupsrId",
                table: "Matches",
                columns: new[] { "TeamLocalId", "TeamVisitorId", "GroupsrId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamLocalId_TeamVisitorId_GroupsrId",
                table: "Matches");
        }
    }
}
