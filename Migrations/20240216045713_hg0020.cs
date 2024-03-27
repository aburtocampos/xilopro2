using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class hg0020 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GroupsGroup_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "GroupsGroup_ID",
                table: "Matches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsGroup_ID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GroupsGroup_ID",
                table: "Matches",
                column: "GroupsGroup_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_GroupsGroup_ID",
                table: "Matches",
                column: "GroupsGroup_ID",
                principalTable: "Groups",
                principalColumn: "Group_ID");
        }
    }
}
