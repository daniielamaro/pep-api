using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ClinicaTipoConsultaConfiguration : IEntityTypeConfiguration<ClinicaConsultaTipo>
    {
        public void Configure(EntityTypeBuilder<ClinicaConsultaTipo> builder)
        {
            builder.HasKey(u => new { u.ConsultaId, u.ClinicaId });

            builder.HasOne(u => u.Clinica)
                   .WithMany(b => b.ConsultaTipos)
                   .HasForeignKey(c => c.ClinicaId);

            builder.HasOne(u => u.ConsultaTipo)
                   .WithMany(b => b.ClinicasConsulta)
                   .HasForeignKey(c => c.ConsultaId);
                    
        }
    }
}
