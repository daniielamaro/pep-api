using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class updatemedicamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intervalo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Medicamentos");

            migrationBuilder.AddColumn<int>(
                name: "NumIntervalo",
                table: "Medicamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumQuantidade",
                table: "Medicamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OutraQuantidade",
                table: "Medicamentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutroIntervalo",
                table: "Medicamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoIntervalo",
                table: "Medicamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoQuantidade",
                table: "Medicamentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumIntervalo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "NumQuantidade",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "OutraQuantidade",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "OutroIntervalo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "TipoIntervalo",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "TipoQuantidade",
                table: "Medicamentos");

            migrationBuilder.AddColumn<string>(
                name: "Intervalo",
                table: "Medicamentos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Quantidade",
                table: "Medicamentos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
