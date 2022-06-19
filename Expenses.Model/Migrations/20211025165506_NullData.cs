using Microsoft.EntityFrameworkCore.Migrations;

namespace Expenses.Model.Migrations
{
    public partial class NullData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase");

            migrationBuilder.AlterColumn<int>(
                name: "FormatId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase",
                column: "FormatId",
                principalTable: "Format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase");

            migrationBuilder.AlterColumn<int>(
                name: "FormatId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "ProductPurchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Brand_BrandId",
                table: "ProductPurchase",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPurchase_Format_FormatId",
                table: "ProductPurchase",
                column: "FormatId",
                principalTable: "Format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
