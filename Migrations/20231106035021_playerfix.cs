using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class playerfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Players",
                newName: "Player_Email");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Player_FNC",
                table: "Players",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Player_Genero",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Player_fifaid",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player_Genero",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Player_fifaid",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Player_Email",
                table: "Players",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Player_FNC",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
