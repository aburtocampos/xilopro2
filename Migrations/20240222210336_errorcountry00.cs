using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class errorcountry00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_State_ID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_State_ID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "State_ID",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "IdState",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdState",
                table: "Cities",
                column: "IdState");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_IdState",
                table: "Cities",
                column: "IdState",
                principalTable: "States",
                principalColumn: "State_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_IdState",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IdState",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IdState",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "State_ID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_State_ID",
                table: "Cities",
                column: "State_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_State_ID",
                table: "Cities",
                column: "State_ID",
                principalTable: "States",
                principalColumn: "State_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
