using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class DocumentoVerificacionUsuarioConfig : IEntityTypeConfiguration<DocumentoVerificacionUsuario>
    {
        public void Configure(EntityTypeBuilder<DocumentoVerificacionUsuario> builder)
        {
            builder.ToTable("DocumentosVerificacionUsuario");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.TipoDocumento)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(d => d.NombreArchivo)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(d => d.RutaArchivo)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(d => d.Estado)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(d => d.ObservacionRevision)
                .HasMaxLength(500);

            builder.HasOne(d => d.Usuario)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
