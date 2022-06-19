using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Model.Migrations
{
    public partial class ProductInProductPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchase_ProductId",
                table: "ProductPurchase",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Product_ProductId",
                table: "ProductPurchase",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Product_ProductId",
                table: "ProductPurchase");

            migrationBuilder.DropIndex(
                name: "IX_ProductPurchase_ProductId",
                table: "ProductPurchase");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductPurchase");
        }
    }
}
