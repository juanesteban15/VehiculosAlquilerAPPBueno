using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class ReservaConfig : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.FechaInicio)
                .IsRequired();

            builder.Property(r => r.FechaFin)
                .IsRequired();

            builder.Property(r => r.PrecioTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(r => r.Vehiculo)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(r => r.Cliente)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(r => r.Estado)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(r => r.Tarifa)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(r => r.MetodoPago)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
