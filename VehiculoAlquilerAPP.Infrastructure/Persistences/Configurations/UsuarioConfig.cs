using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Apellido)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(u => u.Email)
               .IsUnique();

            builder.Property(u => u.Pais)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.TipoCuenta)
                .HasMaxLength(20)
                .HasDefaultValue("Persona")
                .IsRequired();

            builder.Property(u => u.NombreEmpresa)
                .HasMaxLength(120);

            builder.Property(u => u.NitEmpresa)
                .HasMaxLength(40);

            builder.Property(u => u.FotoPerfilRuta)
                .HasMaxLength(500);

            builder.HasOne(u => u.Estado)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            // Teléfonos como owned entity (valor dentro del mismo registro)
            builder.OwnsMany(u => u.Telefonos, t =>
            {
                t.ToTable("UsuarioTelefonos");
                t.Property(x => x.Numero).HasMaxLength(20).IsRequired();
                t.Property(x => x.Tipo).HasMaxLength(20).IsRequired();
            });
        }
    }
}
