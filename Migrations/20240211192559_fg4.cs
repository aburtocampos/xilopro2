using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Torneo_ID",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches",
                column: "TeamLocalId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches",
                column: "TeamVisitorId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Torneo_ID",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamLocalId",
                table: "Matches",
                column: "TeamLocalId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamVisitorId",
                table: "Matches",
                column: "TeamVisitorId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
