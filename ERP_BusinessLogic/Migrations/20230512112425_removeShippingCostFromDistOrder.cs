using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class removeShippingCostFromDistOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "TB_DistributionOrder");

            migrationBuilder.AlterColumn<int>(
                name: "IncreaseMode",
                table: "TB_FMS_Account",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IncreaseMode",
                table: "TB_FMS_Account",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "TB_DistributionOrder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
