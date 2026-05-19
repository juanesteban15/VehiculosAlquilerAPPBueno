using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class VehiculoFotoConfig : IEntityTypeConfiguration<VehiculoFoto>
    {
        public void Configure(EntityTypeBuilder<VehiculoFoto> builder)
        {
            builder.ToTable("VehiculoFotos");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.RutaArchivo)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.NombreArchivo)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(f => f.Vehiculo)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
