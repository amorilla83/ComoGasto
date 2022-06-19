using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Model.Migrations
{
    public partial class lasPriceInDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LastPrice",
                table: "ProductDetails",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPrice",
                table: "ProductDetails");
        }
    }
}
