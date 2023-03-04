using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class ManufacturingOrderUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ManufacturingOrderDetails");

            migrationBuilder.DropTable(
                name: "TB_ManufacturingOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ManufacturingOrder",
                columns: table => new
                {
                    manufactoringOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    leadTime_per_Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productManufacturedId = table.Column<int>(type: "int", nullable: false),
                    qtyToProduce = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ManufacturingOrder", x => x.manufactoringOrderId);
                    table.ForeignKey(
                        name: "FK_TB_ManufacturingOrder_TB_Product",
                        column: x => x.productManufacturedId,
                        principalTable: "TB_Product",
                        principalColumn: "productId",
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
                        principalTable: "TB_ManufacturingOrder",
                        principalColumn: "manufactoringOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ManufacturingOrderDetails_TB_RawMaterial",
                        column: x => x.rawMaterialId,
                        principalTable: "TB_RawMaterial",
                        principalColumn: "materialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ManufacturingOrder_productManufacturedId",
                table: "TB_ManufacturingOrder",
                column: "productManufacturedId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ManufacturingOrderDetails_rawMaterialId",
                table: "TB_ManufacturingOrderDetails",
                column: "rawMaterialId");
        }
    }
}
