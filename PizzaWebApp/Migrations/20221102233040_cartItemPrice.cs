using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaWebApp.Migrations
{
    public partial class cartItemPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CartItemTotal",
                table: "Cart",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartItemTotal",
                table: "Cart");
        }
    }
}
