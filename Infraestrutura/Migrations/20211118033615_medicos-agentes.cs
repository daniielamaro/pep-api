using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class medicosagentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnfermeiroId",
                table: "Medicamentos",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Medicamentos",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Exames",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EnfermeiroId",
                table: "Consultas",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Consultas",
                nullable: true);

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
                    Senha = table.Column<string>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_EnfermeiroId",
                table: "Medicamentos",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_MedicoId",
                table: "Medicamentos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_MedicoId",
                table: "Exames",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_EnfermeiroId",
                table: "Consultas",
                column: "EnfermeiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoId",
                table: "Consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentesAdministrativos_ClinicaId",
                table: "AgentesAdministrativos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentesAdministrativos_FotoPerfilId",
                table: "AgentesAdministrativos",
                column: "FotoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_ClinicaId",
                table: "Enfermeiros",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_FotoPerfilId",
                table: "Enfermeiros",
                column: "FotoPerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_ClinicaId",
                table: "Medicos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_FotoPerfilId",
                table: "Medicos",
                column: "FotoPerfilId");

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

            migrationBuilder.DropTable(
                name: "AgentesAdministrativos");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_EnfermeiroId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_MedicoId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Exames_MedicoId",
                table: "Exames");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_EnfermeiroId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_MedicoId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "EnfermeiroId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Exames");

            migrationBuilder.DropColumn(
                name: "EnfermeiroId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Consultas");
        }
    }
}
