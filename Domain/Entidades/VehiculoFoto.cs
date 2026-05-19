namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class VehiculoFoto
    {
        public int Id { get; private set; }
        public Vehiculo Vehiculo { get; private set; } = default!;
        public string RutaArchivo { get; private set; } = default!;
        public string NombreArchivo { get; private set; } = default!;
        public int Orden { get; private set; }
        public DateTime FechaSubida { get; private set; }

        protected VehiculoFoto() { }

        public VehiculoFoto(Vehiculo vehiculo, string rutaArchivo, string nombreArchivo, int orden)
        {
            Vehiculo = vehiculo ?? throw new ArgumentNullException(nameof(vehiculo));

            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta de la foto es obligatoria.", nameof(rutaArchivo));

            if (string.IsNullOrWhiteSpace(nombreArchivo))
                throw new ArgumentException("El nombre de la foto es obligatorio.", nameof(nombreArchivo));

            RutaArchivo = rutaArchivo.Trim();
            NombreArchivo = nombreArchivo.Trim();
            Orden = orden;
            FechaSubida = DateTime.Now;
        }
    }
}
