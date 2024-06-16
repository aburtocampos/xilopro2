using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class jorndas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_SelectedCategoryIds_Player_Dorsal",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Teamid_Player_Dorsal",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "JornadasFin",
                table: "CorrectionActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JornadasInicio",
                table: "CorrectionActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_SelectedCategoryIds_Player_Dorsal_Teamid",
                table: "Players",
                columns: new[] { "SelectedCategoryIds", "Player_Dorsal", "Teamid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamid",
                table: "Players",
                column: "Teamid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_SelectedCategoryIds_Player_Dorsal_Teamid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Teamid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "JornadasFin",
                table: "CorrectionActions");

            migrationBuilder.DropColumn(
                name: "JornadasInicio",
                table: "CorrectionActions");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SelectedCategoryIds_Player_Dorsal",
                table: "Players",
                columns: new[] { "SelectedCategoryIds", "Player_Dorsal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamid_Player_Dorsal",
                table: "Players",
                columns: new[] { "Teamid", "Player_Dorsal" },
                unique: true);
        }
    }
}
