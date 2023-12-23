using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fixteampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_AspNetUsers_AppUsersId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_AppUsersId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "AppUsersId",
                table: "Positions");

            migrationBuilder.AlterColumn<int>(
                name: "Team_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUsersId",
                table: "Positions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Team_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AppUsersId",
                table: "Positions",
                column: "AppUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_AspNetUsers_AppUsersId",
                table: "Positions",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
