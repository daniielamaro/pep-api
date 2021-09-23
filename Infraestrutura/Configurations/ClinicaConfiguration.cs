﻿using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Configurations
{
    public class ClinicaConfiguration : IEntityTypeConfiguration<Clinica>
    {
        public void Configure(EntityTypeBuilder<Clinica> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.NomeClinica)
                .IsRequired();

            builder
                .Property(u => u.Endereco)
                .IsRequired();

            builder.HasMany(u => u.ExameTipos);



            builder.HasMany(u => u.ConsultaTipos);
                    
        }


    }
}
