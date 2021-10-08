﻿using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.Quantidade)
                .IsRequired();

            builder
                .Property(u => u.Quantidade)
                .IsRequired();

            builder
                .Property(u => u.Intervalo)
                .IsRequired();
        }
    }
}
