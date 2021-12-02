using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class inicial : Migration
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
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: false),
                    Bairro = table.Column<string>(nullable: true),
                    Localidade = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
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
                name: "Clinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    NomeClinica = table.Column<string>(nullable: false),
                    EnderecoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinicas_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentesAdministrativos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    FotoPerfilId = table.Column<Guid>(nullable: true),
                    CPF = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: false),
                    ClinicaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentesAdministrativos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentesAdministrativos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AgentesAdministrativos_Arquivos_FotoPerfilId",
                        column: x => x.FotoPerfilId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Enfermeiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    FotoPerfilId = table.Column<Guid>(nullable: true),
                    COREM = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: false),
                    ClinicaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enfermeiros_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Enfermeiros_Arquivos_FotoPerfilId",
                        column: x => x.FotoPerfilId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    FotoPerfilId = table.Column<Guid>(nullable: true),
                    CRM = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    ClinicaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Medicos_Arquivos_FotoPerfilId",
                        column: x => x.FotoPerfilId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    Publico = table.Column<bool>(nullable: false),
                    DiaRealizacao = table.Column<DateTime>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    EnfermeiroId = table.Column<Guid>(nullable: true),
                    MedicoId = table.Column<Guid>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultas_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    MedicoId = table.Column<Guid>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    NumQuantidade = table.Column<int>(nullable: false),
                    TipoQuantidade = table.Column<string>(nullable: true),
                    OutraQuantidade = table.Column<string>(nullable: true),
                    NumIntervalo = table.Column<int>(nullable: false),
                    TipoIntervalo = table.Column<string>(nullable: true),
                    OutroIntervalo = table.Column<string>(nullable: true),
                    Publico = table.Column<bool>(nullable: false),
                    TipoCadastro = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<Guid>(nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataTermino = table.Column<DateTime>(nullable: true),
                    UsoContinuo = table.Column<bool>(nullable: false),
                    EnfermeiroId = table.Column<Guid>(nullable: true),
                    MedicoId = table.Column<Guid>(nullable: true),
                    PacienteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Enfermeiros_EnfermeiroId",
                        column: x => x.EnfermeiroId,
                        principalTable: "Enfermeiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Arquivos_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentesAdministrativos_ClinicaId",
                table: "AgentesAdministrativos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentesAdministrativos_FotoPerfilId",
                table: "AgentesAdministrativos",
                column: "FotoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinicas_EnderecoId",
                table: "Clinicas",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_EnfermeiroId",
                table: "Consultas",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoId",
                table: "Consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_TipoId",
                table: "Consultas",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_ClinicaId",
                table: "Enfermeiros",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_FotoPerfilId",
                table: "Enfermeiros",
                column: "FotoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_MedicoId",
                table: "Exames",
                column: "MedicoId");

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
                name: "IX_Medicamentos_EnfermeiroId",
                table: "Medicamentos",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_MedicoId",
                table: "Medicamentos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_PacienteId",
                table: "Medicamentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_ReceitaId",
                table: "Medicamentos",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_ClinicaId",
                table: "Medicos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_FotoPerfilId",
                table: "Medicos",
                column: "FotoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_FotoPerfilId",
                table: "Pacientes",
                column: "FotoPerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentesAdministrativos");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "TiposConsultas");

            migrationBuilder.DropTable(
                name: "TiposExames");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
