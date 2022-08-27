using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Model.Migrations
{
    public partial class ProductPurchaseDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "ProductPurchase",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "ProductPurchase");
        }
    }
}
