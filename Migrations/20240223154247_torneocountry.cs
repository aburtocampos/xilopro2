using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneocountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_CountryId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IdState",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedCategoryIds",
                table: "Torneos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Torneos_SelectedCategoryIds_Torneo_Name",
                table: "Torneos",
                columns: new[] { "SelectedCategoryIds", "Torneo_Name" },
                unique: true,
                filter: "[SelectedCategoryIds] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId_State_Name",
                table: "States",
                columns: new[] { "CountryId", "State_Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdState_City_Name",
                table: "Cities",
                columns: new[] { "IdState", "City_Name" },
                unique: true,
                filter: "[IdState] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Torneos_SelectedCategoryIds_Torneo_Name",
                table: "Torneos");

            migrationBuilder.DropIndex(
                name: "IX_States_CountryId_State_Name",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IdState_City_Name",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedCategoryIds",
                table: "Torneos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdState",
                table: "Cities",
                column: "IdState");
        }
    }
}
