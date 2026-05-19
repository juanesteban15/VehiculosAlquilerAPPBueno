using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class VehiculoConfig : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("Vehiculos");
            builder.HasKey(v => v.Placa);

            builder.Property(v => v.Placa)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(v => v.Modelo)
                .IsRequired();

            builder.HasOne(v => v.Marca)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.Categoria)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.TipoVehiculo)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.TipoCombustible)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.SistemaTransmision)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.Estado)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(v => v.Propietario)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            // Colores como owned entity
            builder.OwnsMany(v => v.Colores, c =>
            {
                c.ToTable("VehiculoColores", table =>
                    table.HasCheckConstraint("CK_VehiculoColores_Orden", "[Orden] BETWEEN 1 AND 3"));
                c.Property(x => x.Nombre).HasMaxLength(30).IsRequired();
                c.Property(x => x.ColorVehiculoId);
                c.Property(x => x.Orden).IsRequired();
                c.HasIndex("VehiculoPlaca", "Orden").IsUnique();
            });
        }
    }
}
