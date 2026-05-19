namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class DocumentoVehiculoArchivo
    {
        public int Id { get; private set; }
        public Vehiculo Vehiculo { get; private set; } = default!;
        public string TipoDocumento { get; private set; } = default!;
        public string NombreArchivo { get; private set; } = default!;
        public string RutaArchivo { get; private set; } = default!;
        public DateTime FechaSubida { get; private set; }

        protected DocumentoVehiculoArchivo() { }

        public DocumentoVehiculoArchivo(Vehiculo vehiculo, string tipoDocumento, string nombreArchivo, string rutaArchivo)
        {
            Vehiculo = vehiculo ?? throw new ArgumentNullException(nameof(vehiculo));

            if (string.IsNullOrWhiteSpace(tipoDocumento))
                throw new ArgumentException("El tipo de documento es obligatorio.", nameof(tipoDocumento));

            if (string.IsNullOrWhiteSpace(nombreArchivo))
                throw new ArgumentException("El nombre del archivo es obligatorio.", nameof(nombreArchivo));

            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta del archivo es obligatoria.", nameof(rutaArchivo));

            TipoDocumento = tipoDocumento.Trim();
            NombreArchivo = nombreArchivo.Trim();
            RutaArchivo = rutaArchivo.Trim();
            FechaSubida = DateTime.Now;
        }
    }
}
