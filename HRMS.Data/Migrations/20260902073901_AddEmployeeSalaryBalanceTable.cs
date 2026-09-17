using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Data.Migrations
{
    public partial class AddEmployeeSalaryBalanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "EmployeeSalaryBalances",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    AmountBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryBalances", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropTable(
                name: "EmployeeSalaryBalances");
        }
    }
}
