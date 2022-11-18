using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaWebApp.Migrations
{
    public partial class EditingOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Orders",
                newName: "PizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PizzaId",
                table: "Orders",
                newName: "CartId");
        }
    }
}
