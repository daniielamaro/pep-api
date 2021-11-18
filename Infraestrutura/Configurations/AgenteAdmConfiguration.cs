using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configurations
{
    public class AgenteAdmConfiguration : IEntityTypeConfiguration<AgenteAdministrativo>
    {
        public void Configure(EntityTypeBuilder<AgenteAdministrativo> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.FotoPerfil)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.CPF)
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .IsRequired();

            builder.HasOne(u => u.Clinica)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
