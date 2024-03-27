using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class j002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Team_ID",
                table: "GroupDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupsGroup_ID",
                table: "GroupDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Team_ID",
                table: "GroupDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupsGroup_ID",
                table: "GroupDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Teams_Team_ID",
                table: "GroupDetails",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
