using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class MensajeChatConfig : IEntityTypeConfiguration<MensajeChat>
    {
        public void Configure(EntityTypeBuilder<MensajeChat> builder)
        {
            builder.ToTable("MensajesChat");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Texto).HasMaxLength(1000).IsRequired();
            builder.Property(m => m.FechaEnvio).IsRequired();

            builder.HasOne(m => m.Conversacion)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(m => m.Autor)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
