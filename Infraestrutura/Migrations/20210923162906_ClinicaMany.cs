using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestrutura.Migrations
{
    public partial class ClinicaMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaConsultaTipos_Clinicas_ClinicaId",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId1",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                table: "ClinicaExameTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId",
                table: "ClinicaExameTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropIndex(
                name: "IX_ClinicaExameTipos_ClinicaId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropIndex(
                name: "IX_ClinicaExameTipos_ExameTipoId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropIndex(
                name: "IX_ClinicaConsultaTipos_ClinicaId",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropIndex(
                name: "IX_ClinicaConsultaTipos_ConsultaTipoId1",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropColumn(
                name: "ClinicaId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropColumn(
                name: "ExameTipoId1",
                table: "ClinicaExameTipos");

            migrationBuilder.DropColumn(
                name: "ClinicaId",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropColumn(
                name: "ConsultaTipoId1",
                table: "ClinicaConsultaTipos");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId",
                principalTable: "TiposConsultas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                table: "ClinicaExameTipos",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId",
                table: "ClinicaExameTipos",
                column: "ExameTipoId",
                principalTable: "TiposExames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId",
                table: "ClinicaConsultaTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                table: "ClinicaExameTipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId",
                table: "ClinicaExameTipos");

            migrationBuilder.AddColumn<Guid>(
                name: "ClinicaId1",
                table: "ClinicaExameTipos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExameTipoId1",
                table: "ClinicaExameTipos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClinicaId",
                table: "ClinicaConsultaTipos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConsultaTipoId1",
                table: "ClinicaConsultaTipos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ClinicaId1",
                table: "ClinicaExameTipos",
                column: "ClinicaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaExameTipos_ExameTipoId1",
                table: "ClinicaExameTipos",
                column: "ExameTipoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ClinicaId",
                table: "ClinicaConsultaTipos",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaConsultaTipos_ConsultaTipoId1",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaConsultaTipos_Clinicas_ClinicaId",
                table: "ClinicaConsultaTipos",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId",
                principalTable: "TiposConsultas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaConsultaTipos_TiposConsultas_ConsultaTipoId1",
                table: "ClinicaConsultaTipos",
                column: "ConsultaTipoId1",
                principalTable: "TiposConsultas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId",
                table: "ClinicaExameTipos",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_Clinicas_ClinicaId1",
                table: "ClinicaExameTipos",
                column: "ClinicaId1",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId",
                table: "ClinicaExameTipos",
                column: "ExameTipoId",
                principalTable: "TiposExames",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicaExameTipos_TiposExames_ExameTipoId1",
                table: "ClinicaExameTipos",
                column: "ExameTipoId1",
                principalTable: "TiposExames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
