using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Players_Player_ID",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player_ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Players_Player_ID",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Players_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Categories_Category_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_Position_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Team_ID",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameColumn(
                name: "Playerid",
                table: "Parents",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "User_fifafid",
                table: "AspNetUsers",
                newName: "Player_fifaid");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Team_ID",
                table: "Player",
                newName: "IX_Player_Team_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Position_ID",
                table: "Player",
                newName: "IX_Player_Position_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Countryid",
                table: "Player",
                newName: "IX_Player_Countryid");

            migrationBuilder.RenameIndex(
                name: "IX_Players_CorrectionAction_ID",
                table: "Player",
                newName: "IX_Player_CorrectionAction_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Category_ID",
                table: "Player",
                newName: "IX_Player_Category_ID");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player_Dorsal",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player_Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "User_Genero",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_UserId",
                table: "PlayerFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Team_ID",
                table: "AspNetUsers",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID",
                table: "AspNetUsers",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Player_Player_ID",
                table: "Lineups",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Player_Player_ID",
                table: "Matches",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
                name: "FK_Player_CorrectionActions_CorrectionAction_ID",
                table: "Player",
                column: "CorrectionAction_ID",
                principalTable: "CorrectionActions",
                principalColumn: "CorrectionAction_ID");

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
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_Player_Player_ID",
                table: "PlayerFiles",
                column: "Player_ID",
                principalTable: "Player",
                principalColumn: "Player_ID");
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

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Player_Player_ID",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Player_Player_ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Player_Player_ID",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Categories_Category_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_CorrectionActions_CorrectionAction_ID",
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
                name: "FK_PlayerFiles_AspNetUsers_UserId",
                table: "PlayerFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_Player_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_PlayerFiles_UserId",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Team_ID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlayerFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Player_Dorsal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Player_Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "User_Genero",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Parents",
                newName: "Playerid");

            migrationBuilder.RenameColumn(
                name: "Player_fifaid",
                table: "AspNetUsers",
                newName: "User_fifafid");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Team_ID",
                table: "Players",
                newName: "IX_Players_Team_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Position_ID",
                table: "Players",
                newName: "IX_Players_Position_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Countryid",
                table: "Players",
                newName: "IX_Players_Countryid");

            migrationBuilder.RenameIndex(
                name: "IX_Player_CorrectionAction_ID",
                table: "Players",
                newName: "IX_Players_CorrectionAction_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Player_Category_ID",
                table: "Players",
                newName: "IX_Players_Category_ID");

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Players_Player_ID",
                table: "Lineups",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player_ID",
                table: "Matches",
                column: "Player_ID",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Categories_Category_ID",
                table: "Players",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                table: "Players",
                column: "CorrectionAction_ID",
                principalTable: "CorrectionActions",
                principalColumn: "CorrectionAction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "Country_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_Position_ID",
                table: "Players",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Team_ID",
                table: "Players",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
