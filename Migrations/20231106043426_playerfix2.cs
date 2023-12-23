using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class playerfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parent_ImageCedula",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerFiles",
                columns: table => new
                {
                    PlayerFiles_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerFiles_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerFiles_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Player_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFiles", x => x.PlayerFiles_ID);
                    table.ForeignKey(
                        name: "FK_PlayerFiles_Players_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Players",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFiles_Player_ID",
                table: "PlayerFiles",
                column: "Player_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFiles");

            migrationBuilder.DropColumn(
                name: "Parent_ImageCedula",
                table: "Parents");
        }
    }
}
