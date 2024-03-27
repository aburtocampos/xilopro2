using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class hg003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_Position_ID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Team_ID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Position_ID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Team_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Positionid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Teamid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Positionid",
                table: "Players",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Teamid",
                table: "Players",
                column: "Teamid");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_Positionid",
                table: "Players",
                column: "Positionid",
                principalTable: "Positions",
                principalColumn: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Teamid",
                table: "Players",
                column: "Teamid",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_Positionid",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Teamid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Positionid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Teamid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Positionid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Teamid",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Position_ID",
                table: "Players",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Team_ID",
                table: "Players",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_Position_ID",
                table: "Players",
                column: "Position_ID",
                principalTable: "Positions",
                principalColumn: "Position_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Team_ID",
                table: "Players",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }
    }
}
