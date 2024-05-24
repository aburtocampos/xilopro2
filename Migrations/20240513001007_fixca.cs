using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class fixca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectionActionPlayer");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectionActions_PlayerId",
                table: "CorrectionActions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectionActions_Players_PlayerId",
                table: "CorrectionActions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Player_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectionActions_Players_PlayerId",
                table: "CorrectionActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectionActions_PlayerId",
                table: "CorrectionActions");

            migrationBuilder.CreateTable(
                name: "CorrectionActionPlayer",
                columns: table => new
                {
                    CorrectionActionsCorrectionAction_ID = table.Column<int>(type: "int", nullable: false),
                    Player_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectionActionPlayer", x => new { x.CorrectionActionsCorrectionAction_ID, x.Player_ID });
                    table.ForeignKey(
                        name: "FK_CorrectionActionPlayer_CorrectionActions_CorrectionActionsCorrectionAction_ID",
                        column: x => x.CorrectionActionsCorrectionAction_ID,
                        principalTable: "CorrectionActions",
                        principalColumn: "CorrectionAction_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorrectionActionPlayer_Players_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Players",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectionActionPlayer_Player_ID",
                table: "CorrectionActionPlayer",
                column: "Player_ID");
        }
    }
}
