using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Tipo)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
