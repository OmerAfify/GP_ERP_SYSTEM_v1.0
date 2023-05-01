using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class DistributionOrderUpdated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbDistributionOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDistributionOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_DistributionOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    totalQty = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    orderingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    expectedArrivalDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    orderStatusId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DistributionOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distributionOrder_PK_Distributor",
                        column: x => x.DistributorId,
                        principalTable: "TB_Distributor",
                        principalColumn: "distributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_DistributionOrder_TbDistributionOrderStatus_orderStatusId",
                        column: x => x.orderStatusId,
                        principalTable: "TbDistributionOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_DistributionOrderDetails",
                columns: table => new
                {
                    distributionOrderId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("COM_PK_distributionOrderId_productId", x => new { x.distributionOrderId, x.productId });
                    table.ForeignKey(
                        name: "FK_DistributionOrderDetails_PK_DistributionOrder",
                        column: x => x.distributionOrderId,
                        principalTable: "TB_DistributionOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionOrderDetails_PK_Products",
                        column: x => x.productId,
                        principalTable: "TB_Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_DistributionOrder_DistributorId",
                table: "TB_DistributionOrder",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DistributionOrder_orderStatusId",
                table: "TB_DistributionOrder",
                column: "orderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DistributionOrderDetails_productId",
                table: "TB_DistributionOrderDetails",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DistributionOrderDetails");

            migrationBuilder.DropTable(
                name: "TB_DistributionOrder");

            migrationBuilder.DropTable(
                name: "TbDistributionOrderStatus");
        }
    }
}
