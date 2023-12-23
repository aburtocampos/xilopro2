using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fic1000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Team_ID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Position_ID1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_ID1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Position_ID1",
                table: "AspNetUsers",
                column: "Position_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Team_ID1",
                table: "AspNetUsers",
                column: "Team_ID1");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Team_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position_ID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Team_ID1",
                table: "AspNetUsers");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Team_ID",
                table: "AspNetUsers",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
