using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class cedulaunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Player_Email",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Player_Cedula",
                table: "Players",
                column: "Player_Cedula",
                unique: true,
                filter: "[Player_Cedula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Player_Email",
                table: "Players",
                column: "Player_Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Player_Cedula",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Player_Email",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Player_Email",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
