using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class hg001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "GroupsGroup_ID",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GroupsrId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GroupsrId",
                table: "Matches",
                column: "GroupsrId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups",
                column: "Group_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups",
                columns: new[] { "torneoId", "Group_Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupsrId",
                table: "Matches",
                column: "GroupsrId",
                principalTable: "Groups",
                principalColumn: "Group_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupsrId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GroupsrId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Group_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupsrId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "GroupsGroup_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId",
                table: "Groups",
                column: "torneoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
