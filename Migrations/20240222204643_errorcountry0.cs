using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class errorcountry0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_Country_ID",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "Country_ID",
                table: "States",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_States_Country_ID",
                table: "States",
                newName: "IX_States_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_CountryId",
                table: "States",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Country_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_CountryId",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "States",
                newName: "Country_ID");

            migrationBuilder.RenameIndex(
                name: "IX_States_CountryId",
                table: "States",
                newName: "IX_States_Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_Country_ID",
                table: "States",
                column: "Country_ID",
                principalTable: "Countries",
                principalColumn: "Country_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
