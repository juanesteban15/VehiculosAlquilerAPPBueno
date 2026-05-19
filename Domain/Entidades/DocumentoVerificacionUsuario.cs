namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class DocumentoVerificacionUsuario
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; } = default!;
        public string TipoDocumento { get; private set; } = default!;
        public string NombreArchivo { get; private set; } = default!;
        public string RutaArchivo { get; private set; } = default!;
        public string Estado { get; private set; } = default!;
        public DateTime FechaSubida { get; private set; }
        public DateTime? FechaRevision { get; private set; }
        public string? ObservacionRevision { get; private set; }

        protected DocumentoVerificacionUsuario() { }

        public DocumentoVerificacionUsuario(
            Usuario usuario,
            string tipoDocumento,
            string nombreArchivo,
            string rutaArchivo)
        {
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(tipoDocumento))
                throw new ArgumentException("El tipo de documento es obligatorio.", nameof(tipoDocumento));

            if (string.IsNullOrWhiteSpace(nombreArchivo))
                throw new ArgumentException("El nombre del archivo es obligatorio.", nameof(nombreArchivo));

            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta del archivo es obligatoria.", nameof(rutaArchivo));

            TipoDocumento = tipoDocumento.Trim();
            NombreArchivo = nombreArchivo.Trim();
            RutaArchivo = rutaArchivo.Trim();
            Estado = "Pendiente";
            FechaSubida = DateTime.Now;
        }

        public void Aprobar()
        {
            Estado = "Aprobado";
            FechaRevision = DateTime.Now;
            ObservacionRevision = null;
        }

        public void Rechazar(string observacion)
        {
            Estado = "Rechazado";
            FechaRevision = DateTime.Now;
            ObservacionRevision = observacion;
        }
    }
}
