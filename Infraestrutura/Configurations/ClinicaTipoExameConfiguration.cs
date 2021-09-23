using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ClinicaTipoExameConfiguration : IEntityTypeConfiguration<ClinicaTipoExames>
    {
        public void Configure(EntityTypeBuilder<ClinicaTipoExames> builder)
        {
            builder.HasKey(u => new { u.ExameId, u.ClinicaId });

            builder.HasOne(u => u.Clinica)
                   .WithMany(b => b.ExameTipos)
                   .HasForeignKey(c => c.ClinicaId);

            builder.HasOne(u => u.ExameTipo)
                   .WithMany(b => b.ClinicasExame)
                   .HasForeignKey(c => c.ExameId);
                    
        }
    }
}
