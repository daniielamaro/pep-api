using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class updatemedicamentocadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicaConsultaTipos");

            migrationBuilder.DropTable(
                name: "ClinicaExameTipos");

            migrationBuilder.AddColumn<bool>(
                name: "InputByUser",
                table: "Medicamentos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceitaId",
                table: "Medicamentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoCadastro",
                table: "Medicamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_ReceitaId",
                table: "Medicamentos",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Arquivos_ReceitaId",
                table: "Medicamentos",
                column: "ReceitaId",
                principalTable: "Arquivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Arquivos_ReceitaId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_ReceitaId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "InputByUser",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "ReceitaId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "TipoCadastro",
                table: "Medicamentos");

            migrationBuilder.CreateTable(
                name: "ClinicaConsultaTipos",
                columns: table => new
                {
                    ConsultaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicaConsultaTipos", x => new { x.ConsultaId, x.ClinicaId });
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "TiposConsultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicaExameTipos",
                columns: table => new
                {
                    ExameId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicaExameTipos", x => new { x.ExameId, x.ClinicaId });
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_TiposExames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "TiposExames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ClinicaId",
                table: "ClinicaConsultaTipos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ClinicaId",
                table: "ClinicaExameTipos",
                column: "ClinicaId");
        }
    }
}
