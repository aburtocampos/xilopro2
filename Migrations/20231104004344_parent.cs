using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class parent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Parent_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Parent_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Parent_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Parent_Cedula = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Parent_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country_ID = table.Column<int>(type: "int", nullable: true),
                    State_ID = table.Column<int>(type: "int", nullable: true),
                    City_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Parent_ID);
                    table.ForeignKey(
                        name: "FK_Parents_Cities_City_ID",
                        column: x => x.City_ID,
                        principalTable: "Cities",
                        principalColumn: "City_ID");
                    table.ForeignKey(
                        name: "FK_Parents_Countries_Country_ID",
                        column: x => x.Country_ID,
                        principalTable: "Countries",
                        principalColumn: "Country_ID");
                    table.ForeignKey(
                        name: "FK_Parents_States_State_ID",
                        column: x => x.State_ID,
                        principalTable: "States",
                        principalColumn: "State_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_City_ID",
                table: "Parents",
                column: "City_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Country_ID",
                table: "Parents",
                column: "Country_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_State_ID",
                table: "Parents",
                column: "State_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parents");
        }
    }
}
