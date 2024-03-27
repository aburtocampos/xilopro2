using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class hg002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "Country_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players",
                column: "Countryid",
                principalTable: "Countries",
                principalColumn: "Country_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
