using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class rawMaterialssInventoryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hasReachedROP",
                table: "TB_FinishedProductsInventory",
                newName: "HasReachedROP");

            migrationBuilder.AddColumn<bool>(
                name: "HasReachedROP",
                table: "TB_RawMaterialsInventory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasReachedROP",
                table: "TB_RawMaterialsInventory");

            migrationBuilder.RenameColumn(
                name: "HasReachedROP",
                table: "TB_FinishedProductsInventory",
                newName: "hasReachedROP");
        }
    }
}
