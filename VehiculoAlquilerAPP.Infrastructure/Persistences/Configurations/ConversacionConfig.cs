using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class ConversacionConfig : IEntityTypeConfiguration<Conversacion>
    {
        public void Configure(EntityTypeBuilder<Conversacion> builder)
        {
            builder.ToTable("Conversaciones");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FechaCreacion).IsRequired();

            builder.HasOne(c => c.Reserva)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(c => c.Cliente)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(c => c.Propietario)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
