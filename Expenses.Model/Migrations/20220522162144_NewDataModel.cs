using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Model.Migrations
{
    public partial class NewDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Product_ProductId",
                table: "ProductPurchase");

            migrationBuilder.DropTable(
                name: "BrandFormat");

            migrationBuilder.DropTable(
                name: "BrandProduct");

            migrationBuilder.DropIndex(
                name: "IX_ProductPurchase_BrandId",
                table: "ProductPurchase");

            migrationBuilder.DropIndex(
                name: "IX_ProductPurchase_FormatId",
                table: "ProductPurchase");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductPurchase");

            migrationBuilder.DropColumn(
                name: "FormatId",
                table: "ProductPurchase");

            migrationBuilder.RenameColumn(
                name: "IdPurchase",
                table: "Purchase",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductPurchase",
                newName: "ProductDetailId");

            migrationBuilder.RenameColumn(
                name: "ProductPurchaseId",
                table: "ProductPurchase",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchase_ProductId",
                table: "ProductPurchase",
                newName: "IX_ProductPurchase_ProductDetailId");

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: true),
                    FormatId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetails_Format_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Format",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_BrandId",
                table: "ProductDetails",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_FormatId",
                table: "ProductDetails",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Purchase",
                newName: "IdPurchase");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "ProductPurchase",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductPurchase",
                newName: "ProductPurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPurchase_ProductDetailId",
                table: "ProductPurchase",
                newName: "IX_ProductPurchase_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormatId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchase_BrandId",
                table: "ProductPurchase",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchase_FormatId",
                table: "ProductPurchase",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandFormat_FormatListId",
                table: "BrandFormat",
                column: "FormatListId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductListId",
                table: "BrandProduct",
                column: "ProductListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase",
                column: "FormatId",
                principalTable: "Format",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Product_ProductId",
                table: "ProductPurchase",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
