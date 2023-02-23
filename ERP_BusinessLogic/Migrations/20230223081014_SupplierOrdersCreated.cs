using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class SupplierOrdersCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbOrderStatus_Supplier",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrderStatus_Supplier", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "TbOrder_Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    TotalQty = table.Column<int>(type: "int", nullable: false),
                    SubTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    OrderingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrder_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbOrder_Supplier_TB_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "TB_Supplier",
                        principalColumn: "supplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbOrder_Supplier_TbOrderStatus_Supplier_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "TbOrderStatus_Supplier",
                        principalColumn: "OrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbOrderDetails_Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedRawMaterials_MaterialId = table.Column<int>(type: "int", nullable: true),
                    OrderedRawMaterials_MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderedRawMaterials_SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TbOrder_SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrderDetails_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbOrderDetails_Supplier_TbOrder_Supplier_TbOrder_SupplierId",
                        column: x => x.TbOrder_SupplierId,
                        principalTable: "TbOrder_Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbOrder_Supplier_OrderStatusId",
                table: "TbOrder_Supplier",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TbOrder_Supplier_SupplierId",
                table: "TbOrder_Supplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_TbOrderDetails_Supplier_TbOrder_SupplierId",
                table: "TbOrderDetails_Supplier",
                column: "TbOrder_SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbOrderDetails_Supplier");

            migrationBuilder.DropTable(
                name: "TbOrder_Supplier");

            migrationBuilder.DropTable(
                name: "TbOrderStatus_Supplier");
        }
    }
}
