using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC5_Task.Migrations
{
    public partial class AddWorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Hours",
                table: "EmployeeProjects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "EmployeeProjects");
        }
    }
}
