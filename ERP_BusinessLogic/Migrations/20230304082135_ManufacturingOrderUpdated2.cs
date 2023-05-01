using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class ManufacturingOrderUpdated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbManufacturingStatus",
                columns: table => new
                {
                    statusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbManufacturingStatus", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "TbManufacturingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductManufacturedId = table.Column<int>(type: "int", nullable: false),
                    QtyToManufacture = table.Column<int>(type: "int", nullable: false),
                    ManufacturingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManufacturingStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbManufacturingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbManufacturingOrders_TB_Product_ProductManufacturedId",
                        column: x => x.ProductManufacturedId,
                        principalTable: "TB_Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbManufacturingOrders_TbManufacturingStatus_ManufacturingStatusId",
                        column: x => x.ManufacturingStatusId,
                        principalTable: "TbManufacturingStatus",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ManufacturingOrderDetails",
                columns: table => new
                {
                    manfactoringOrderId = table.Column<int>(type: "int", nullable: false),
                    rawMaterialId = table.Column<int>(type: "int", nullable: false),
                    rawMaterialQtyUsed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ManufacturingOrderDetails", x => new { x.manfactoringOrderId, x.rawMaterialId });
                    table.ForeignKey(
                        name: "FK_TB_ManufacturingOrderDetails_TB_ManufacturingOrder",
                        column: x => x.manfactoringOrderId,
                        principalTable: "TbManufacturingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ManufacturingOrderDetails_TB_RawMaterial",
                        column: x => x.rawMaterialId,
                        principalTable: "TB_RawMaterial",
                        principalColumn: "materialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ManufacturingOrderDetails_rawMaterialId",
                table: "TB_ManufacturingOrderDetails",
                column: "rawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TbManufacturingOrders_ManufacturingStatusId",
                table: "TbManufacturingOrders",
                column: "ManufacturingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TbManufacturingOrders_ProductManufacturedId",
                table: "TbManufacturingOrders",
                column: "ProductManufacturedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ManufacturingOrderDetails");

            migrationBuilder.DropTable(
                name: "TbManufacturingOrders");

            migrationBuilder.DropTable(
                name: "TbManufacturingStatus");
        }
    }
}
