using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ConsultaTipoConfiguration : IEntityTypeConfiguration<ConsultaTipo>
    {
        public void Configure(EntityTypeBuilder<ConsultaTipo> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Nome)
                .IsRequired();

        }
    }
}
