using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ArquivoConfiguration : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.Tipo)
                .IsRequired();
        }
    }
}
