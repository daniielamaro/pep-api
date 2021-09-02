using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.FotoPerfil)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(u => u.Nome)
                .IsRequired();

            builder
                .Property(u => u.Cpf)
                .IsRequired();

            builder
                .Property(u => u.Rg)
                .IsRequired();

            builder
                .Property(u => u.DataNasc)
                .IsRequired();

            builder
                .Property(u => u.Endereco)
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .IsRequired();

            builder.HasMany(u => u.Exames)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Consultas)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
