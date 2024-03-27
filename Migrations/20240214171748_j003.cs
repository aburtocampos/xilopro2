using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class j003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails");

            migrationBuilder.DropIndex(
                name: "IX_GroupDetails_Team_ID",
                table: "GroupDetails");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "GroupDetails");

            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "GroupDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "teamId",
                table: "GroupDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_teamId",
                table: "GroupDetails",
                column: "teamId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Teams_teamId",
                table: "GroupDetails",
                column: "teamId",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Teams_teamId",
                table: "GroupDetails");

            migrationBuilder.DropIndex(
                name: "IX_GroupDetails_teamId",
                table: "GroupDetails");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "GroupDetails");

            migrationBuilder.DropColumn(
                name: "teamId",
                table: "GroupDetails");

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "GroupDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_Team_ID",
                table: "GroupDetails",
                column: "Team_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }
    }
}
