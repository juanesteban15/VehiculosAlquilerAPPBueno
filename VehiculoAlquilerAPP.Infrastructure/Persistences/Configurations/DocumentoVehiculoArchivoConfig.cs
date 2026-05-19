using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Configurations
{
    public class DocumentoVehiculoArchivoConfig : IEntityTypeConfiguration<DocumentoVehiculoArchivo>
    {
        public void Configure(EntityTypeBuilder<DocumentoVehiculoArchivo> builder)
        {
            builder.ToTable("DocumentosVehiculo");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.TipoDocumento)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(d => d.NombreArchivo)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(d => d.RutaArchivo)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(d => d.Vehiculo)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
