using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class season : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds",
                table: "Torneos");

            migrationBuilder.DropIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds_Torneo_Season",
                table: "Torneos");

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds_Torneo_Season",
                table: "Torneos",
                columns: new[] { "Torneo_Name", "SelectedCategoryIds", "Torneo_Season" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds_Torneo_Season",
                table: "Torneos");

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds",
                table: "Torneos",
                columns: new[] { "Torneo_Name", "SelectedCategoryIds" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds_Torneo_Season",
                table: "Torneos",
                columns: new[] { "Torneo_Name", "SelectedCategoryIds", "Torneo_Season" },
                unique: true,
                filter: "Torneo_Season IS NULL");
        }
    }
}
