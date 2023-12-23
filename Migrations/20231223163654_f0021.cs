using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f0021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PositionId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Players",
                newName: "Team_ID1");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Players",
                newName: "Team_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                newName: "IX_Players_Team_ID1");

            migrationBuilder.AddColumn<int>(
                name: "Position_ID",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position_ID1",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Position_ID1",
                table: "Players",
                column: "Position_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_Position_ID1",
                table: "Players",
                column: "Position_ID1",
                principalTable: "Positions",
                principalColumn: "Position_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_Team_ID1",
                table: "Players",
                column: "Team_ID1",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_Position_ID1",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_Team_ID1",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Position_ID1",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Position_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Position_ID1",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Team_ID1",
                table: "Players",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "Team_ID",
                table: "Players",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Team_ID1",
                table: "Players",
                newName: "IX_Players_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                column: "PositionId");

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
    }
}
