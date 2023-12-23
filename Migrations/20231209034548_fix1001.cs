using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fix1001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID1",
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
                name: "FK_Player_CorrectionActions_CorrectionAction_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Countries_Country_ID",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_AppUserId",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_PlayerFiles_AppUserId",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Team_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_Country_ID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "AppUserId",
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
                name: "Player_fifaid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Team_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country_ID",
                table: "Player");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Players",
                newName: "Player_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Player_CorrectionAction_ID",
                table: "Players",
                newName: "IX_Players_CorrectionAction_ID");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayerFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player_ID",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Parents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player_ID",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Player_Genero",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "Cityid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Countryid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SelectedCategoryIds",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stateid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Countryid",
                table: "Players",
                column: "Countryid");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

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
                name: "FK_Players_Positions_PositionId",
                table: "Players",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropIndex(
                name: "IX_Parents_Player_ID",
                table: "Parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Countryid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PositionId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_TeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerFiles");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "PlayerFiles");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Player_ID",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Cityid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Countryid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SelectedCategoryIds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Stateid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameColumn(
                name: "Player_PhoneNumber",
                table: "Player",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Players_CorrectionAction_ID",
                table: "Player",
                newName: "IX_Player_CorrectionAction_ID");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PlayerFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player_Dorsal",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player_Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player_fifaid",
                table: "AspNetUsers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Player_Genero",
                table: "Player",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Country_ID",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_AppUserId",
                table: "PlayerFiles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Position_ID1",
                table: "AspNetUsers",
                column: "Position_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Team_ID1",
                table: "AspNetUsers",
                column: "Team_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Country_ID",
                table: "Player",
                column: "Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID1",
                table: "AspNetUsers",
                column: "Position_ID1",
                principalTable: "Positions",
                principalColumn: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID1",
                table: "AspNetUsers",
                column: "Team_ID1",
                principalTable: "Teams",
                principalColumn: "Team_ID");

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
                name: "FK_Player_CorrectionActions_CorrectionAction_ID",
                table: "Player",
                column: "CorrectionAction_ID",
                principalTable: "CorrectionActions",
                principalColumn: "CorrectionAction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Countries_Country_ID",
                table: "Player",
                column: "Country_ID",
                principalTable: "Countries",
                principalColumn: "Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFiles_AspNetUsers_AppUserId",
                table: "PlayerFiles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
