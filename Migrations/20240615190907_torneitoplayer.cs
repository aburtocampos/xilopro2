using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneitoplayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "torneoid",
                table: "Players",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "torneoid",
                table: "Players");
        }
    }
}
