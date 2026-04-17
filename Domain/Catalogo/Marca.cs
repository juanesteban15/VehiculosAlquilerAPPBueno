using VehiculosAlquilerApp.Domain.Exceptions.Catalogo;

namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class Marca
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public TipoVehiculo TipoVehiculo { get; private set; }

        // Constructor para Entity Framework
        protected Marca() { }

        public Marca(TipoVehiculo tipoVehiculo, string nombre)
        {
            TipoVehiculo = tipoVehiculo ?? throw new ArgumentNullException(nameof(tipoVehiculo), "El tipo de vehículo es obligatorio.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la marca es requerido.", nameof(nombre));

            Nombre = nombre.Trim();
        }

        public void CambiarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nuevo nombre no es válido.");

            Nombre = nuevoNombre.Trim();
        }
    }
}