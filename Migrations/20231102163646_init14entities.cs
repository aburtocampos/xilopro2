using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class init14entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_ID);
                });

            migrationBuilder.CreateTable(
                name: "CorrectionActions",
                columns: table => new
                {
                    CorrectionAction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrectionAction_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorrectionAction_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectionActions", x => x.CorrectionAction_ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Country_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Country_ID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Group_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Group_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Group_ID);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Position_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Position_ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Team_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Team_Estadio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team_Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Team_ID);
                });

            migrationBuilder.CreateTable(
                name: "Torneos",
                columns: table => new
                {
                    Torneo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Torneo_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Torneo_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Torneo_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Torneo_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Torneo_Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Torneo_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Torneo_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneos", x => x.Torneo_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    User_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    User_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Cedula = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    UserTypeofRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Status = table.Column<bool>(type: "bit", nullable: false),
                    User_CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Countryid = table.Column<int>(type: "int", nullable: false),
                    Stateid = table.Column<int>(type: "int", nullable: false),
                    Cityid = table.Column<int>(type: "int", nullable: false),
                    Category_ID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Categories_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "Categories",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate:ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    State_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.State_ID);
                    table.ForeignKey(
                        name: "FK_States_Countries_Country_ID",
                        column: x => x.Country_ID,
                        principalTable: "Countries",
                        principalColumn: "Country_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupDetails",
                columns: table => new
                {
                    GroupDetail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: false),
                    MatchesWon = table.Column<int>(type: "int", nullable: false),
                    MatchesTied = table.Column<int>(type: "int", nullable: false),
                    MatchesLost = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GroupsGroup_ID = table.Column<int>(type: "int", nullable: false),
                    Team_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDetails", x => x.GroupDetail_ID);
                    table.ForeignKey(
                        name: "FK_GroupDetails_Groups_GroupsGroup_ID",
                        column: x => x.GroupsGroup_ID,
                        principalTable: "Groups",
                        principalColumn: "Group_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDetails_Teams_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Teams",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    City_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.City_ID);
                    table.ForeignKey(
                        name: "FK_Cities_States_State_ID",
                        column: x => x.State_ID,
                        principalTable: "States",
                        principalColumn: "State_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Player_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Player_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Player_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player_Dorsal = table.Column<int>(type: "int", nullable: false),
                    Player_FNC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player_Status = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Cedula = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Player_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category_ID = table.Column<int>(type: "int", nullable: false),
                    Team_ID = table.Column<int>(type: "int", nullable: false),
                    Position_ID = table.Column<int>(type: "int", nullable: false),
                    State_ID = table.Column<int>(type: "int", nullable: true),
                    City_ID = table.Column<int>(type: "int", nullable: true),
                    CorrectionAction_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Player_ID);
                    table.ForeignKey(
                        name: "FK_Players_Categories_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "Categories",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Cities_City_ID",
                        column: x => x.City_ID,
                        principalTable: "Cities",
                        principalColumn: "City_ID");
                    table.ForeignKey(
                        name: "FK_Players_CorrectionActions_CorrectionAction_ID",
                        column: x => x.CorrectionAction_ID,
                        principalTable: "CorrectionActions",
                        principalColumn: "CorrectionAction_ID");
                    table.ForeignKey(
                        name: "FK_Players_Positions_Position_ID",
                        column: x => x.Position_ID,
                        principalTable: "Positions",
                        principalColumn: "Position_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_States_State_ID",
                        column: x => x.State_ID,
                        principalTable: "States",
                        principalColumn: "State_ID");
                    table.ForeignKey(
                        name: "FK_Players_Teams_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Teams",
                        principalColumn: "Team_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    Lineup_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lineup_IsTitular = table.Column<bool>(type: "bit", nullable: false),
                    EntraPor = table.Column<int>(type: "int", nullable: false),
                    Salepor = table.Column<int>(type: "int", nullable: false),
                    MinEntra = table.Column<int>(type: "int", nullable: false),
                    MinSale = table.Column<int>(type: "int", nullable: false),
                    Player_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.Lineup_ID);
                    table.ForeignKey(
                        name: "FK_Lineups_Players_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Players",
                        principalColumn: "Player_ID");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Match_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GoalsLocal = table.Column<int>(type: "int", nullable: false),
                    GoalsVisitor = table.Column<int>(type: "int", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Player_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupsGroup_ID = table.Column<int>(type: "int", nullable: false),
                    Team_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Match_ID);
                    table.ForeignKey(
                        name: "FK_Matches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Groups_GroupsGroup_ID",
                        column: x => x.GroupsGroup_ID,
                        principalTable: "Groups",
                        principalColumn: "Group_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Players",
                        principalColumn: "Player_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team_ID",
                        column: x => x.Team_ID,
                        principalTable: "Teams",
                        principalColumn: "Team_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Category_ID",
                table: "AspNetUsers",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Category_Name",
                table: "Categories",
                column: "Category_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_State_ID",
                table: "Cities",
                column: "State_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Country_Name",
                table: "Countries",
                column: "Country_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_GroupsGroup_ID",
                table: "GroupDetails",
                column: "GroupsGroup_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_Team_ID",
                table: "GroupDetails",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsTorneo_TorneosTorneo_ID",
                table: "GroupsTorneo",
                column: "TorneosTorneo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_Player_ID",
                table: "Lineups",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GroupsGroup_ID",
                table: "Matches",
                column: "GroupsGroup_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player_ID",
                table: "Matches",
                column: "Player_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team_ID",
                table: "Matches",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Category_ID",
                table: "Players",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_City_ID",
                table: "Players",
                column: "City_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CorrectionAction_ID",
                table: "Players",
                column: "CorrectionAction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Position_ID",
                table: "Players",
                column: "Position_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_State_ID",
                table: "Players",
                column: "State_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Team_ID",
                table: "Players",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Position_Name",
                table: "Positions",
                column: "Position_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_Country_ID",
                table: "States",
                column: "Country_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Team_Name",
                table: "Teams",
                column: "Team_Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GroupDetails");

            migrationBuilder.DropTable(
                name: "GroupsTorneo");

            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Torneos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CorrectionActions");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
