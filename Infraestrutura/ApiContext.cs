using Dominio.Entities;
using Infraestrutura.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura
{
    public class ApiContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<ExameTipo> TiposExames { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<ConsultaTipo> TiposConsultas { get; set; }
        public DbSet<ClinicaConsultaTipo> ClinicaConsultaTipos { get; set; }
        public DbSet<ClinicaTipoExames> ClinicaExameTipos { get; set; }

        private readonly string ConectionString = "Server=35.184.198.60;Port=5432;Database=postgres;User Id=unisuam;Password=@Unisuam1234;";

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public ApiContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(ConectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new ArquivoConfiguration());
            modelBuilder.ApplyConfiguration(new ExameConfiguration());
            modelBuilder.ApplyConfiguration(new ExameTipoConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaTipoConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicaConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentoConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicaTipoExameConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicaTipoConsultaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
