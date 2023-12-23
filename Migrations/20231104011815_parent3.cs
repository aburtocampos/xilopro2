using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class parent3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parent_ID",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Parents",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Parent_ID",
                table: "Players",
                column: "Parent_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Parents_Parent_ID",
                table: "Players",
                column: "Parent_ID",
                principalTable: "Parents",
                principalColumn: "Parent_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Parents_Parent_ID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Parent_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Parent_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Parents");
        }
    }
}
