﻿// <auto-generated />
using System;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infraestrutura.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Dominio.Entities.Arquivo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Binario")
                        .HasColumnType("bytea");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Arquivos");
                });

            modelBuilder.Entity("Dominio.Entities.Clinica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeClinica")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clinicas");
                });

            modelBuilder.Entity("Dominio.Entities.ClinicaConsultaTipo", b =>
                {
                    b.Property<Guid>("ConsultaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClinicaId")
                        .HasColumnType("uuid");

                    b.HasKey("ConsultaId", "ClinicaId");

                    b.HasIndex("ClinicaId");

                    b.ToTable("ClinicaConsultaTipos");
                });

            modelBuilder.Entity("Dominio.Entities.ClinicaTipoExames", b =>
                {
                    b.Property<Guid>("ExameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClinicaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("ExameId", "ClinicaId");

                    b.HasIndex("ClinicaId");

                    b.ToTable("ClinicaExameTipos");
                });

            modelBuilder.Entity("Dominio.Entities.Consulta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Observacoes")
                        .HasColumnType("text");

                    b.Property<Guid?>("PacienteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Resumo")
                        .HasColumnType("text");

                    b.Property<Guid?>("TipoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("TipoId");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("Dominio.Entities.ConsultaTipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TiposConsultas");
                });

            modelBuilder.Entity("Dominio.Entities.Exame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DiaRealizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Observacoes")
                        .HasColumnType("text");

                    b.Property<Guid?>("PacienteId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Publico")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ResultadoId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TipoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("ResultadoId");

                    b.HasIndex("TipoId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("Dominio.Entities.ExameTipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TiposExames");
                });

            modelBuilder.Entity("Dominio.Entities.Medicamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DataTermino")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Intervalo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PacienteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Quantidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("UsoContinuo")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("Medicamentos");
                });

            modelBuilder.Entity("Dominio.Entities.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataNasc")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("FotoPerfilId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FotoPerfilId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Dominio.Entities.ClinicaConsultaTipo", b =>
                {
                    b.HasOne("Dominio.Entities.Clinica", "Clinica")
                        .WithMany("ConsultaTipos")
                        .HasForeignKey("ClinicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.ConsultaTipo", "ConsultaTipo")
                        .WithMany("ClinicasConsulta")
                        .HasForeignKey("ConsultaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entities.ClinicaTipoExames", b =>
                {
                    b.HasOne("Dominio.Entities.Clinica", "Clinica")
                        .WithMany("ExameTipos")
                        .HasForeignKey("ClinicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.ExameTipo", "ExameTipo")
                        .WithMany("ClinicasExame")
                        .HasForeignKey("ExameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entities.Consulta", b =>
                {
                    b.HasOne("Dominio.Entities.Paciente", null)
                        .WithMany("Consultas")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Dominio.Entities.ConsultaTipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Dominio.Entities.Exame", b =>
                {
                    b.HasOne("Dominio.Entities.Paciente", null)
                        .WithMany("Exames")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Dominio.Entities.Arquivo", "Resultado")
                        .WithMany()
                        .HasForeignKey("ResultadoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Dominio.Entities.ExameTipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Dominio.Entities.Medicamento", b =>
                {
                    b.HasOne("Dominio.Entities.Paciente", null)
                        .WithMany("Medicamentos")
                        .HasForeignKey("PacienteId");
                });

            modelBuilder.Entity("Dominio.Entities.Paciente", b =>
                {
                    b.HasOne("Dominio.Entities.Arquivo", "FotoPerfil")
                        .WithMany()
                        .HasForeignKey("FotoPerfilId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
