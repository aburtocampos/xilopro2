using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f0023 : Migration
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

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Players",
                newName: "Team_ID");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Players",
                newName: "Position_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                newName: "IX_Players_Team_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                newName: "IX_Players_Position_ID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameIndex(
                name: "IX_Players_Team_ID",
                table: "Players",
                newName: "IX_Players_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_Position_ID",
                table: "Players",
                newName: "IX_Players_PositionId");

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
