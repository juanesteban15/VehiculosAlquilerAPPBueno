using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class NotificacionConfig : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable("Notificaciones");
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Titulo).HasMaxLength(120).IsRequired();
            builder.Property(n => n.Mensaje).HasMaxLength(500).IsRequired();
            builder.Property(n => n.Ruta).HasMaxLength(250);
            builder.Property(n => n.FechaCreacion).IsRequired();

            builder.HasOne(n => n.Usuario)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
