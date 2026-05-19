using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class TarifaConfig : IEntityTypeConfiguration<Tarifa>
    {
        public void Configure(EntityTypeBuilder<Tarifa> builder)
        {
            builder.ToTable("Tarifas");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.PrecioPorDia)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(t => t.FechaInicio)
                .IsRequired();

            builder.HasOne(t => t.Vehiculo)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
