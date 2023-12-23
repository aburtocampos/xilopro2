using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f0026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Players_PlayerId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_PlayerFiles_PlayerId",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Parents_PlayerId",
                table: "Parents");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Player_ID",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Lineups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents",
                column: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Players_Player_ID",
                table: "PlayerFiles",
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

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Players_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "Parents");

            migrationBuilder.AlterColumn<int>(
                name: "Player_ID",
                table: "Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Player_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Player_ID",
                table: "Lineups",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_PlayerId",
                table: "PlayerFiles",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_PlayerId",
                table: "Parents",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Players_PlayerId",
                table: "Parents",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Players_PlayerId",
                table: "PlayerFiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
