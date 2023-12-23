using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class parent5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Parent_Address",
                table: "Parents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents",
                column: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Parent_Address",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "Parents");

            migrationBuilder.AddColumn<string>(
                name: "Parent_ID",
                table: "Players",
                type: "nvarchar(450)",
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
    }
}
