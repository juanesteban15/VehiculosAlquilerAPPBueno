namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class SistemaTransmision
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public TipoVehiculo TipoVehiculo { get; private set; }

        // Constantes para evitar errores de dedo en la lógica de negocio
        public const string Manual = "MANUAL";
        public const string Automatico = "AUTOMATICO";

        // Constructor para Entity Framework
        protected SistemaTransmision() { }

        public SistemaTransmision(TipoVehiculo tipoVehiculo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del sistema de transmisión es requerido.", nameof(nombre));

            // CORRECCIÓN: Asignación correcta a la propiedad de la clase
            TipoVehiculo = tipoVehiculo ?? throw new ArgumentNullException(nameof(tipoVehiculo), "El tipo de vehículo es obligatorio.");
            Nombre = nombre.Trim().ToUpper();
        }
    }
}