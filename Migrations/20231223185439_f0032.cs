using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class f0032 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Countryid",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Countryid",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Countryid",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Country_ID",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Country_ID",
                table: "Players",
                column: "Country_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_Country_ID",
                table: "Players",
                column: "Country_ID",
                principalTable: "Countries",
                principalColumn: "Country_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_Country_ID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Country_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Country_ID",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Countryid",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Countryid",
                table: "Players",
                column: "Countryid");

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
