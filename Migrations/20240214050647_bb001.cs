using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class bb001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups",
                columns: new[] { "torneoId", "Group_Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups",
                column: "Group_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId",
                table: "Groups",
                column: "torneoId");
        }
    }
}
