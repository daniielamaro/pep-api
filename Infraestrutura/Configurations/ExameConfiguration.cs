using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ExameConfiguration : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Tipo)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Resultado)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
