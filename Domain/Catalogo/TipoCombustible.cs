namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class TipoCombustible
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public TipoVehiculo TipoVehiculo { get; private set; }

        // Constantes para facilitar el uso en la capa de aplicación
        public const string Gasolina = "GASOLINA";
        public const string Diesel = "DIESEL";
        public const string Electrico = "ELECTRICO";
        public const string Hibrido = "HIBRIDO";

        // Constructor para Entity Framework
        protected TipoCombustible() { }

        public TipoCombustible(TipoVehiculo tipoVehiculo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del tipo de combustible es requerido.", nameof(nombre));

            TipoVehiculo = tipoVehiculo ?? throw new ArgumentNullException(nameof(tipoVehiculo), "El tipo de vehículo es obligatorio.");
            Nombre = nombre.Trim().ToUpper();
        }
    }
}