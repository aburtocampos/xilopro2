using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class _4gh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_Team_ID",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "GroupsTorneo");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Team_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Team_ID",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "LocalTeam_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VisitorTeam_ID",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Torneo_ID",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LocalTeam_ID",
                table: "Matches",
                column: "LocalTeam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_VisitorTeam_ID",
                table: "Matches",
                column: "VisitorTeam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups",
                column: "Torneo_ID",
                principalTable: "Torneos",
                principalColumn: "Torneo_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_LocalTeam_ID",
                table: "Matches",
                column: "LocalTeam_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_VisitorTeam_ID",
                table: "Matches",
                column: "VisitorTeam_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Torneos_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_VisitorTeam_ID",
                
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_VisitorTeam_ID",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Torneo_ID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LocalTeam_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "VisitorTeam_ID",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Torneo_ID",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "Team_ID",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupsTorneo",
                columns: table => new
                {
                    GroupsGroup_ID = table.Column<int>(type: "int", nullable: false),
                    TorneosTorneo_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsTorneo", x => new { x.GroupsGroup_ID, x.TorneosTorneo_ID });
                    table.ForeignKey(
                        name: "FK_GroupsTorneo_Groups_GroupsGroup_ID",
                        column: x => x.GroupsGroup_ID,
                        principalTable: "Groups",
                        principalColumn: "Group_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsTorneo_Torneos_TorneosTorneo_ID",
                        column: x => x.TorneosTorneo_ID,
                        principalTable: "Torneos",
                        principalColumn: "Torneo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team_ID",
                table: "Matches",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsTorneo_TorneosTorneo_ID",
                table: "GroupsTorneo",
                column: "TorneosTorneo_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_Team_ID",
                table: "Matches",
                column: "Team_ID",
                principalTable: "Teams",
                principalColumn: "Team_ID");
        }
    }
}
