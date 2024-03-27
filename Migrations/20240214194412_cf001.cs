using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class cf001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupDetails_GroupsGroup_ID",
                table: "GroupDetails");

            migrationBuilder.DropColumn(
                name: "GroupsGroup_ID",
                table: "GroupDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId",
                table: "Groups",
                column: "torneoId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_groupId_teamId",
                table: "GroupDetails",
                columns: new[] { "groupId", "teamId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Groups_groupId",
                table: "GroupDetails",
                column: "groupId",
                principalTable: "Groups",
                principalColumn: "Group_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Groups_groupId",
                table: "GroupDetails");

            migrationBuilder.DropIndex(
                name: "IX_Groups_torneoId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupDetails_groupId_teamId",
                table: "GroupDetails");

            migrationBuilder.AddColumn<int>(
                name: "GroupsGroup_ID",
                table: "GroupDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_torneoId_Group_Name",
                table: "Groups",
                columns: new[] { "torneoId", "Group_Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_GroupsGroup_ID",
                table: "GroupDetails",
                column: "GroupsGroup_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                table: "GroupDetails",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID");
        }
    }
}
