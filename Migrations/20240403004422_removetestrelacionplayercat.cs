using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class removetestrelacionplayercat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Categories_CategoryId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_CategoryId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_CategoryId",
                table: "Players",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Categories_CategoryId",
                table: "Players",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Category_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
