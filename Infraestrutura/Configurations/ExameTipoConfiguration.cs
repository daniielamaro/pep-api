using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ExameTipoConfiguration : IEntityTypeConfiguration<ExameTipo>
    {
        public void Configure(EntityTypeBuilder<ExameTipo> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Nome)
                .IsRequired();
        }
    }
}
