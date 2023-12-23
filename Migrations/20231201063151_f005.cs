using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Player_Player_ID",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Categories_Category_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Countries_Countryid",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Positions_Position_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Teams_Team_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Player_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Player_Countryid",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_Position_ID",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_Team_ID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Categoriaid",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Cityid",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Countryid",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Stateid",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Player_ID",
                table: "PlayerFiles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles",
                newName: "IX_PlayerFiles_UserId");

            migrationBuilder.RenameColumn(
                name: "Category_ID",
                table: "Player",
                newName: "Country_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Category_ID",
                table: "Player",
                newName: "IX_Player_Country_ID");

            migrationBuilder.RenameColumn(
                name: "Player_ID",
                table: "Parents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents",
                newName: "IX_Parents_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Countries_Country_ID",
                table: "Player",
                column: "Country_ID",
                principalTable: "Countries",
                principalColumn: "Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Countries_Country_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlayerFiles",
                newName: "Player_ID");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFiles_UserId",
                table: "PlayerFiles",
                newName: "IX_PlayerFiles_Player_ID");

            migrationBuilder.RenameColumn(
                name: "Country_ID",
                table: "Player",
                newName: "Category_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Country_ID",
                table: "Player",
                newName: "IX_Player_Category_ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Parents",
                newName: "Player_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                newName: "IX_Parents_Player_ID");

            migrationBuilder.AddColumn<int>(
                name: "Categoriaid",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cityid",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Countryid",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stateid",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_Countryid",
                table: "Player",
                column: "Countryid");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Position_ID",
                table: "Player",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Team_ID",
                table: "Player",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Player_Player_ID",
                table: "Parents",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Categories_Category_ID",
                table: "Player",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Countries_Countryid",
                table: "Player",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "Country_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Positions_Position_ID",
                table: "Player",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Teams_Team_ID",
                table: "Player",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Player_Player_ID",
                table: "PlayerFiles",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID");
        }
    }
}
