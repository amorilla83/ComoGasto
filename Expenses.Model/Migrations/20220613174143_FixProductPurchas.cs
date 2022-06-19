using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Model.Migrations
{
    public partial class FixProductPurchas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase");

            migrationBuilder.AlterColumn<int>(
                name: "ProductDetailId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase");

            migrationBuilder.AlterColumn<int>(
                name: "ProductDetailId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_ProductDetails_ProductDetailId",
                table: "ProductPurchase",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
