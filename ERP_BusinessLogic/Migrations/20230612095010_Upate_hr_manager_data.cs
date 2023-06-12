using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_BusinessLogic.Migrations
{
    public partial class Upate_hr_manager_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_EmployeeTaskDetails_TB_EmployeeDetails",
                table: "TB_EmployeeTaskDetails");

            migrationBuilder.RenameColumn(
                name: "HRPassword",
                table: "TB_HRManagerDetails",
                newName: "Phone");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "TB_HRManagerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "TB_HRManagerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HREmail",
                table: "TB_HRManagerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "TB_HRManagerDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_EmployeeTaskDetails_TB_EmployeeDetails",
                table: "TB_EmployeeTaskDetails",
                column: "EmplyeeId",
                principalTable: "TB_EmployeeDetails",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_EmployeeTaskDetails_TB_EmployeeDetails",
                table: "TB_EmployeeTaskDetails");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "TB_HRManagerDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TB_HRManagerDetails");

            migrationBuilder.DropColumn(
                name: "HREmail",
                table: "TB_HRManagerDetails");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "TB_HRManagerDetails");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "TB_HRManagerDetails",
                newName: "HRPassword");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_EmployeeTaskDetails_TB_EmployeeDetails",
                table: "TB_EmployeeTaskDetails",
                column: "EmplyeeId",
                principalTable: "TB_EmployeeDetails",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
