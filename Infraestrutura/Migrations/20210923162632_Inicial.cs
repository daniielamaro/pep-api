using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: false),
                    Binario = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    NomeClinica = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposConsultas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposConsultas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposExames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposExames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    FotoPerfilId = table.Column<Guid>(nullable: true),
                    Cpf = table.Column<string>(nullable: false),
                    Rg = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    DataNasc = table.Column<DateTime>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Arquivos_FotoPerfilId",
                        column: x => x.FotoPerfilId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClinicaConsultaTipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    ConsultaTipoId1 = table.Column<Guid>(nullable: true),
                    ClinicasId = table.Column<Guid>(nullable: true),
                    ClinicaId = table.Column<Guid>(nullable: true),
                    ConsultaTipoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicaConsultaTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_Clinicas_ClinicasId",
                        column: x => x.ClinicasId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId",
                        column: x => x.ConsultaTipoId,
                        principalTable: "TiposConsultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId1",
                        column: x => x.ConsultaTipoId1,
                        principalTable: "TiposConsultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicaExameTipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    ExameTipoId1 = table.Column<Guid>(nullable: true),
                    ClinicaId1 = table.Column<Guid>(nullable: true),
                    ClinicaId = table.Column<Guid>(nullable: true),
                    ExameTipoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicaExameTipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_Clinicas_ClinicaId1",
                        column: x => x.ClinicaId1,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId",
                        column: x => x.ExameTipoId,
                        principalTable: "TiposExames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId1",
                        column: x => x.ExameTipoId1,
                        principalTable: "TiposExames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    TipoId = table.Column<Guid>(nullable: true),
                    Resumo = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultas_TiposConsultas_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposConsultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    ResultadoId = table.Column<Guid>(nullable: true),
                    TipoId = table.Column<Guid>(nullable: true),
                    Publico = table.Column<bool>(nullable: false),
                    DiaRealizacao = table.Column<DateTime>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exames_Arquivos_ResultadoId",
                        column: x => x.ResultadoId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Exames_TiposExames_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposExames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ClinicaId",
                table: "ClinicaConsultaTipos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ClinicasId",
                table: "ClinicaConsultaTipos",
                column: "ClinicasId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ConsultaTipoId",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ConsultaTipoId1",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ClinicaId",
                table: "ClinicaExameTipos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ClinicaId1",
                table: "ClinicaExameTipos",
                column: "ClinicaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ExameTipoId",
                table: "ClinicaExameTipos",
                column: "ExameTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ExameTipoId1",
                table: "ClinicaExameTipos",
                column: "ExameTipoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_TipoId",
                table: "Consultas",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_PacienteId",
                table: "Exames",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_ResultadoId",
                table: "Exames",
                column: "ResultadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_TipoId",
                table: "Exames",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_FotoPerfilId",
                table: "Pacientes",
                column: "FotoPerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicaConsultaTipos");

            migrationBuilder.DropTable(
                name: "ClinicaExameTipos");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.DropTable(
                name: "TiposConsultas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "TiposExames");

            migrationBuilder.DropTable(
                name: "Arquivos");
        }
    }
}
