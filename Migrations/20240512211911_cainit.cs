using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class cainit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_CorrectionAction_ID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CorrectionAction_ID",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CorrectionActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "CorrectionActions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "CorrectionActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "CorrectionActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectionActionPlayer");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CorrectionActions");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "CorrectionActions");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "CorrectionActions");

            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "CorrectionActions");

            migrationBuilder.AddColumn<int>(
                name: "CorrectionAction_ID",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_CorrectionAction_ID",
                table: "Players",
                column: "CorrectionAction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                table: "Players",
                column: "CorrectionAction_ID",
                principalTable: "CorrectionActions",
                principalColumn: "CorrectionAction_ID");
        }
    }
}
