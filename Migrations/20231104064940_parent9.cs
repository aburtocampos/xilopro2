using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class parent9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID");
        }
    }
}
