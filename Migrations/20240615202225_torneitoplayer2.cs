using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class torneitoplayer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "season",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "season",
                table: "Players");
        }
    }
}
