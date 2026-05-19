using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class ComentarioUsuarioConfig : IEntityTypeConfiguration<ComentarioUsuario>
    {
        public void Configure(EntityTypeBuilder<ComentarioUsuario> builder)
        {
            builder.ToTable("ComentariosUsuario");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Calificacion)
                .IsRequired();

            builder.Property(c => c.Comentario)
                .HasMaxLength(700)
                .IsRequired();

            builder.HasOne(c => c.UsuarioEvaluado)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(c => c.Autor)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
