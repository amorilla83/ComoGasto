using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Model.Migrations
{
    public partial class BrandFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Format",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandProduct",
                columns: table => new
                {
                    BrandListId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductListId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandProduct", x => new { x.BrandListId, x.ProductListId });
                    table.ForeignKey(
                        name: "FK_BrandProduct_Brand_BrandListId",
                        column: x => x.BrandListId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandProduct_Product_ProductListId",
                        column: x => x.ProductListId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandFormat",
                columns: table => new
                {
                    BrandListId = table.Column<int>(type: "INTEGER", nullable: false),
                    FormatListId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandFormat", x => new { x.BrandListId, x.FormatListId });
                    table.ForeignKey(
                        name: "FK_BrandFormat_Brand_BrandListId",
                        column: x => x.BrandListId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandFormat_Format_FormatListId",
                        column: x => x.FormatListId,
                        principalTable: "Format",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandFormat_FormatListId",
                table: "BrandFormat",
                column: "FormatListId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductListId",
                table: "BrandProduct",
                column: "ProductListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandFormat");

            migrationBuilder.DropTable(
                name: "BrandProduct");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
