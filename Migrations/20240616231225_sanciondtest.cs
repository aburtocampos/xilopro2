using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class sanciondtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JornadasFin",
                table: "CorrectionActions");

            migrationBuilder.DropColumn(
                name: "JornadasInicio",
                table: "CorrectionActions");

            migrationBuilder.AddColumn<string>(
                name: "Jornadasasancionar",
                table: "CorrectionActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_torneoid",
                table: "Players",
                column: "torneoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Torneos_torneoid",
                table: "Players",
                column: "torneoid",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Torneos_torneoid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_torneoid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Jornadasasancionar",
                table: "CorrectionActions");

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
        }
    }
}
