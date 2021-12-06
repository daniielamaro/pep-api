using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class correcao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Enfermeiros_EnfermeiroId",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_MedicoId",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Exames_Medicos_MedicoId",
                table: "Exames");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Enfermeiros_EnfermeiroId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Medicos_MedicoId",
                table: "Medicamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Enfermeiros_EnfermeiroId",
                table: "Consultas",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_MedicoId",
                table: "Consultas",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Exames_Medicos_MedicoId",
                table: "Exames",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Enfermeiros_EnfermeiroId",
                table: "Medicamentos",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Medicos_MedicoId",
                table: "Medicamentos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Enfermeiros_EnfermeiroId",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_MedicoId",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Exames_Medicos_MedicoId",
                table: "Exames");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Enfermeiros_EnfermeiroId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Medicos_MedicoId",
                table: "Medicamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Enfermeiros_EnfermeiroId",
                table: "Consultas",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_MedicoId",
                table: "Consultas",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exames_Medicos_MedicoId",
                table: "Exames",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Enfermeiros_EnfermeiroId",
                table: "Medicamentos",
                column: "EnfermeiroId",
                principalTable: "Enfermeiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Medicos_MedicoId",
                table: "Medicamentos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
