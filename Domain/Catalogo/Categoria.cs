using VehiculosAlquilerApp.Domain.Exceptions.Catalogo; // Namespace sugerido para tus excepciones

namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class Categoria
    {
        public int Id { get; private set; }
        public TipoVehiculo TipoVehiculo { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        // Constructor para Entity Framework (opcional, suele ser protected o private)
        protected Categoria() { }

        public Categoria(TipoVehiculo tipoVehiculo, string nombre, string descripcion = null)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoría es requerido.", nameof(nombre));

            TipoVehiculo = tipoVehiculo ?? throw new ArgumentNullException(nameof(tipoVehiculo), "El tipo de vehículo es obligatorio para la categoría.");
            Nombre = nombre.Trim();
            Descripcion = descripcion;
        }

        // Método para actualizar la categoría si fuera necesario (Siguiendo DDD)
        public void Editar(string nuevoNombre, string nuevaDescripcion)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            Nombre = nuevoNombre.Trim();
            Descripcion = nuevaDescripcion;
        }
    }
}