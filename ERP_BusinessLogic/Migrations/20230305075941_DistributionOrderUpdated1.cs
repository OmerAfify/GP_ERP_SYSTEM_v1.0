using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class DistributionOrderUpdated1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DistributionOrderDetails");

            migrationBuilder.DropTable(
                name: "TB_DistributionOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_DistributionOrder",
                columns: table => new
                {
                    distributionOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    distributorId = table.Column<int>(type: "int", nullable: false),
                    expectedArrivalDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    orderStatus = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    orderingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TB_Distr__BAC7F8314DA2F88A", x => x.distributionOrderId);
                    table.ForeignKey(
                        name: "FK_distributionOrder_PK_Distributor",
                        column: x => x.distributorId,
                        principalTable: "TB_Distributor",
                        principalColumn: "distributorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_DistributionOrderDetails",
                columns: table => new
                {
                    distributionOrderId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("COM_PK_distributionOrderId_productId", x => new { x.distributionOrderId, x.productId });
                    table.ForeignKey(
                        name: "FK_DistributionOrderDetails_PK_DistributionOrder",
                        column: x => x.distributionOrderId,
                        principalTable: "TB_DistributionOrder",
                        principalColumn: "distributionOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionOrderDetails_PK_Products",
                        column: x => x.productId,
                        principalTable: "TB_Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_DistributionOrder_distributorId",
                table: "TB_DistributionOrder",
                column: "distributorId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DistributionOrderDetails_productId",
                table: "TB_DistributionOrderDetails",
                column: "productId");
        }
    }
}
