using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Catalogo;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class ColorVehiculoConfig : IEntityTypeConfiguration<ColorVehiculo>
    {
        public void Configure(EntityTypeBuilder<ColorVehiculo> builder)
        {
            builder.ToTable("Colores");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nombre)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Activo)
                .IsRequired();

            builder.HasIndex(c => c.Nombre)
                .IsUnique();
        }
    }
}
