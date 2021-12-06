using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.FotoPerfil)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.CRM)
                .IsRequired();

            builder.HasOne(u => u.Clinica)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Medicamentos)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Exames)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Consultas)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
