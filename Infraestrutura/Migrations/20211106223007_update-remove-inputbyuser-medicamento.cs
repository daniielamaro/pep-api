using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class updateremoveinputbyusermedicamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputByUser",
                table: "Medicamentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InputByUser",
                table: "Medicamentos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
