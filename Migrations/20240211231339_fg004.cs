using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fg004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Torneo_ID",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "torneoId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups",
                column: "Group_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId",
                table: "Groups",
                column: "torneoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Torneos_torneoId",
                table: "Groups",
                column: "torneoId",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Torneos_torneoId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "torneoId",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "Torneo_ID",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID");
        }
    }
}
