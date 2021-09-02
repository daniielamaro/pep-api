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
        public DbSet<ExameTipo> TiposExames { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<ConsultaTipo> TiposConsultas { get; set; }

        private readonly string ConectionString = "Server=;Port=;Database=;User Id=;Password=;";

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
