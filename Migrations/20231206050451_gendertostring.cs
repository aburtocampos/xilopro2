using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class gendertostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlayerFiles",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFiles_UserId",
                table: "PlayerFiles",
                newName: "IX_PlayerFiles_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "User_Genero",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Player_Dorsal",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_AppUserId",
                table: "PlayerFiles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_AppUserId",
                table: "PlayerFiles");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "PlayerFiles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFiles_AppUserId",
                table: "PlayerFiles",
                newName: "IX_PlayerFiles_UserId");

            migrationBuilder.AlterColumn<bool>(
                name: "User_Genero",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Player_Dorsal",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
