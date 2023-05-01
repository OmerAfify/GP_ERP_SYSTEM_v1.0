using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class OrdersTablesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbOrder_Supplier_TB_Supplier_SupplierId",
                table: "TbOrder_Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_TbOrder_Supplier_TbOrderStatus_Supplier_OrderStatusId",
                table: "TbOrder_Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_TbOrderDetails_Supplier_TbOrder_Supplier_TbOrder_SupplierId",
                table: "TbOrderDetails_Supplier");

            migrationBuilder.DropTable(
                name: "TB_SupplyOrderDetails");

            migrationBuilder.DropTable(
                name: "TB_SupplyOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderStatus_Supplier",
                table: "TbOrderStatus_Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderDetails_Supplier",
                table: "TbOrderDetails_Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrder_Supplier",
                table: "TbOrder_Supplier");

            migrationBuilder.RenameTable(
                name: "TbOrderStatus_Supplier",
                newName: "TbOrderStatus_Suppliers");

            migrationBuilder.RenameTable(
                name: "TbOrderDetails_Supplier",
                newName: "TbOrderDetails_Suppliers");

            migrationBuilder.RenameTable(
                name: "TbOrder_Supplier",
                newName: "TbOrder_Suppliers");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrderDetails_Supplier_TbOrder_SupplierId",
                table: "TbOrderDetails_Suppliers",
                newName: "IX_TbOrderDetails_Suppliers_TbOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrder_Supplier_SupplierId",
                table: "TbOrder_Suppliers",
                newName: "IX_TbOrder_Suppliers_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrder_Supplier_OrderStatusId",
                table: "TbOrder_Suppliers",
                newName: "IX_TbOrder_Suppliers_OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderStatus_Suppliers",
                table: "TbOrderStatus_Suppliers",
                column: "OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderDetails_Suppliers",
                table: "TbOrderDetails_Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrder_Suppliers",
                table: "TbOrder_Suppliers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrder_Suppliers_TB_Supplier_SupplierId",
                table: "TbOrder_Suppliers",
                column: "SupplierId",
                principalTable: "TB_Supplier",
                principalColumn: "supplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrder_Suppliers_TbOrderStatus_Suppliers_OrderStatusId",
                table: "TbOrder_Suppliers",
                column: "OrderStatusId",
                principalTable: "TbOrderStatus_Suppliers",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrderDetails_Suppliers_TbOrder_Suppliers_TbOrder_SupplierId",
                table: "TbOrderDetails_Suppliers",
                column: "TbOrder_SupplierId",
                principalTable: "TbOrder_Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbOrder_Suppliers_TB_Supplier_SupplierId",
                table: "TbOrder_Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_TbOrder_Suppliers_TbOrderStatus_Suppliers_OrderStatusId",
                table: "TbOrder_Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_TbOrderDetails_Suppliers_TbOrder_Suppliers_TbOrder_SupplierId",
                table: "TbOrderDetails_Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderStatus_Suppliers",
                table: "TbOrderStatus_Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderDetails_Suppliers",
                table: "TbOrderDetails_Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrder_Suppliers",
                table: "TbOrder_Suppliers");

            migrationBuilder.RenameTable(
                name: "TbOrderStatus_Suppliers",
                newName: "TbOrderStatus_Supplier");

            migrationBuilder.RenameTable(
                name: "TbOrderDetails_Suppliers",
                newName: "TbOrderDetails_Supplier");

            migrationBuilder.RenameTable(
                name: "TbOrder_Suppliers",
                newName: "TbOrder_Supplier");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrderDetails_Suppliers_TbOrder_SupplierId",
                table: "TbOrderDetails_Supplier",
                newName: "IX_TbOrderDetails_Supplier_TbOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrder_Suppliers_SupplierId",
                table: "TbOrder_Supplier",
                newName: "IX_TbOrder_Supplier_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrder_Suppliers_OrderStatusId",
                table: "TbOrder_Supplier",
                newName: "IX_TbOrder_Supplier_OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderStatus_Supplier",
                table: "TbOrderStatus_Supplier",
                column: "OrderStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderDetails_Supplier",
                table: "TbOrderDetails_Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrder_Supplier",
                table: "TbOrder_Supplier",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TB_SupplyOrder",
                columns: table => new
                {
                    supplyOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expectedArrivalDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    orderStatus = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    orderingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    supplierId = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TB_Suppl__A384C3CE2DCABE0D", x => x.supplyOrderId);
                    table.ForeignKey(
                        name: "FK_SupplyOrder_PK_Supplier",
                        column: x => x.supplierId,
                        principalTable: "TB_Supplier",
                        principalColumn: "supplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_SupplyOrderDetails",
                columns: table => new
                {
                    supplyOrderId = table.Column<int>(type: "int", nullable: false),
                    materialId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("COM_PK_supplyOrderId_materialId", x => new { x.supplyOrderId, x.materialId });
                    table.ForeignKey(
                        name: "FK_SupplyOrderDetails_PK_RawMaterial",
                        column: x => x.materialId,
                        principalTable: "TB_RawMaterial",
                        principalColumn: "materialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplyOrderDetails_PK_SupplyOrder",
                        column: x => x.supplyOrderId,
                        principalTable: "TB_SupplyOrder",
                        principalColumn: "supplyOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_SupplyOrder_supplierId",
                table: "TB_SupplyOrder",
                column: "supplierId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SupplyOrderDetails_materialId",
                table: "TB_SupplyOrderDetails",
                column: "materialId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrder_Supplier_TB_Supplier_SupplierId",
                table: "TbOrder_Supplier",
                column: "SupplierId",
                principalTable: "TB_Supplier",
                principalColumn: "supplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrder_Supplier_TbOrderStatus_Supplier_OrderStatusId",
                table: "TbOrder_Supplier",
                column: "OrderStatusId",
                principalTable: "TbOrderStatus_Supplier",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrderDetails_Supplier_TbOrder_Supplier_TbOrder_SupplierId",
                table: "TbOrderDetails_Supplier",
                column: "TbOrder_SupplierId",
                principalTable: "TbOrder_Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
