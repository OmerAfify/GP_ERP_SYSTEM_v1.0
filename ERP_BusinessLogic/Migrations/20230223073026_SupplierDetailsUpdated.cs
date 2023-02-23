using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class SupplierDetailsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "averageDeliveryTime",
                table: "TB_SupplyingMaterialDetails");

  
            migrationBuilder.AddColumn<int>(
                name: "AdverageDeliveryTimeInDays",
                table: "TB_Supplier",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
           name: "AdverageDeliveryTimeInDays",
           table: "TB_Supplier");


            migrationBuilder.AddColumn<DateTime>(
                name: "averageDeliveryTime",
                table: "TB_SupplyingMaterialDetails",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

       
        }
    }
}
