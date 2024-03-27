using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneorepetir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Torneos_SelectedCategoryIds_Torneo_Name",
                table: "Torneos");

            migrationBuilder.AlterColumn<string>(
                name: "Torneo_Season",
                table: "Torneos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds",
                table: "Torneos");

            migrationBuilder.DropIndex(
                name: "IX_Torneos_Torneo_Name_SelectedCategoryIds_Torneo_Season",
                table: "Torneos");

            migrationBuilder.AlterColumn<string>(
                name: "Torneo_Season",
                table: "Torneos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_SelectedCategoryIds_Torneo_Name",
                table: "Torneos",
                columns: new[] { "SelectedCategoryIds", "Torneo_Name" },
                unique: true,
                filter: "[SelectedCategoryIds] IS NOT NULL");
        }
    }
}
