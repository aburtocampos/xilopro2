using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f203 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Country_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_Position_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Team_ID",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Team_ID",
                table: "Players",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "Position_ID",
                table: "Players",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "Country_ID",
                table: "Players",
                newName: "Countryid");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Team_ID",
                table: "Players",
                newName: "IX_Players_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Position_ID",
                table: "Players",
                newName: "IX_Players_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Country_ID",
                table: "Players",
                newName: "IX_Players_Countryid");

            migrationBuilder.AlterColumn<int>(
                name: "Player_ID",
                table: "Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Players",
                newName: "Team_ID");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Players",
                newName: "Position_ID");

            migrationBuilder.RenameColumn(
                name: "Countryid",
                table: "Players",
                newName: "Country_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                newName: "IX_Players_Team_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                newName: "IX_Players_Position_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Countryid",
                table: "Players",
                newName: "IX_Players_Country_ID");

            migrationBuilder.AlterColumn<int>(
                name: "Player_ID",
                table: "Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Country_ID",
                table: "Players",
                column: "Country_ID",
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
