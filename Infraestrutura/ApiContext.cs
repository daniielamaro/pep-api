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
        public DbSet<AgenteAdministrativo> AgentesAdministrativos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Enfermeiro> Enfermeiros { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<ExameTipo> TiposExames { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<ConsultaTipo> TiposConsultas { get; set; }

        private readonly string ConectionString = "Server=35.188.193.179;Port=5432;Database=postgres;User Id=unisuam;Password=@Unisuam123;";

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public ApiContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(ConectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new EnfermeiroConfiguration());
            modelBuilder.ApplyConfiguration(new AgenteAdmConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new ArquivoConfiguration());
            modelBuilder.ApplyConfiguration(new ExameConfiguration());
            modelBuilder.ApplyConfiguration(new ExameTipoConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaTipoConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new ClinicaConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
