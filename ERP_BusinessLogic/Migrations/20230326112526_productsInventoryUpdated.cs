using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class productsInventoryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasReachedROP",
                table: "TB_FinishedProductsInventory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasReachedROP",
                table: "TB_FinishedProductsInventory");
        }
    }
}
