using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xilopro2.Migrations
{
    /// <inheritdoc />
    public partial class xilo45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryIdentityUserCategoria");

            migrationBuilder.DropTable(
                name: "IdentityUserCategoria");

            migrationBuilder.CreateTable(
                name: "UserCategory",
                columns: table => new
                {
                    Category_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategory", x => new { x.User_ID, x.Category_ID });
                    table.ForeignKey(
                        name: "FK_UserCategory_AspNetUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCategory_Categories_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "Categories",
                        principalColumn: "Category_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategory_Category_ID",
                table: "UserCategory",
                column: "Category_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCategory");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "IdentityUserCategoria",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Categoria_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserCategoria_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryIdentityUserCategoria",
                columns: table => new
                {
                    CategoriesCategory_ID = table.Column<int>(type: "int", nullable: false),
                    IdentityUserCategoriasId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIdentityUserCategoria", x => new { x.CategoriesCategory_ID, x.IdentityUserCategoriasId });
                    table.ForeignKey(
                        name: "FK_CategoryIdentityUserCategoria_Categories_CategoriesCategory_ID",
                        column: x => x.CategoriesCategory_ID,
                        principalTable: "Categories",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryIdentityUserCategoria_IdentityUserCategoria_IdentityUserCategoriasId",
                        column: x => x.IdentityUserCategoriasId,
                        principalTable: "IdentityUserCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIdentityUserCategoria_IdentityUserCategoriasId",
                table: "CategoryIdentityUserCategoria",
                column: "IdentityUserCategoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserCategoria_UserId",
                table: "IdentityUserCategoria",
                column: "UserId");
        }
    }
}
