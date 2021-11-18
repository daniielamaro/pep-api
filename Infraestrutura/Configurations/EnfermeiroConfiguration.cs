using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configurations
{
    public class EnfermeiroConfiguration : IEntityTypeConfiguration<Enfermeiro>
    {
        public void Configure(EntityTypeBuilder<Enfermeiro> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.FotoPerfil)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.COREM)
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .IsRequired();

            builder.HasOne(u => u.Clinica)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Medicamentos)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Consultas)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
