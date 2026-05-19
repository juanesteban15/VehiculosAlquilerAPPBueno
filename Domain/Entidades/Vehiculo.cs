
using VehiculosAlquilerAoo.Domain.Exceptions.Vehiculo;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Vehiculo
    {
        // Propiedades en PascalCase y con set privado para proteger el estado
        public string Placa { get; private set; }
        public TipoVehiculo TipoVehiculo { get; private set; }
        public Marca Marca { get; private set; }
        public Categoria Categoria { get; private set; }
        public TipoCombustible TipoCombustible { get; private set; }
        public SistemaTransmision SistemaTransmision { get; private set; }
        public int Modelo { get; private set; }
        public Usuario Propietario { get; private set; }
        public EstadoVehiculo Estado { get; private set; } // Renombrado para mayor claridad
        public DateTime FechaRegistro { get; private set; }

        private readonly List<Color> _colores = new List<Color>();
        public IReadOnlyCollection<Color> Colores => _colores.AsReadOnly();

        protected Vehiculo() { }

        public Vehiculo(
            string placa,
            TipoVehiculo tipoVehiculo,
            Marca marca,
            Categoria categoria,
            TipoCombustible tipoCombustible,
            SistemaTransmision sistemaTransmision,
            int modelo,
            Usuario propietario,
            EstadoVehiculo estadoInitial)
        {
            // Validaciones de nulidad y reglas básicas
            if (string.IsNullOrWhiteSpace(placa))
                throw new VehiculoInvalidoException("La placa es obligatoria.");

            if (modelo < 1900 || modelo > DateTime.Now.Year + 1)
                throw new VehiculoInvalidoException($"El modelo {modelo} no es válido.");

            // Uso de operador null-coalescing para limpieza
            TipoVehiculo = tipoVehiculo ?? throw new VehiculoInvalidoException("Tipo de vehículo requerido.");
            Marca = marca ?? throw new VehiculoInvalidoException("Marca requerida.");
            Categoria = categoria ?? throw new VehiculoInvalidoException("Categoría requerida.");
            TipoCombustible = tipoCombustible ?? throw new VehiculoInvalidoException("Tipo de combustible requerido.");
            SistemaTransmision = sistemaTransmision ?? throw new VehiculoInvalidoException("Sistema de transmisión requerido.");
            Propietario = propietario ?? throw new VehiculoInvalidoException("Propietario requerido.");
            Estado = estadoInitial ?? throw new VehiculoInvalidoException("Estado inicial requerido.");

            // Regla de Negocio: Validación de consistencia entre Marca y Tipo
            // Asumiendo que Marca tiene una propiedad TipoVehiculo o TipoVehiculoId
            if (marca.TipoVehiculo.Id != tipoVehiculo.Id)
            {
                throw new VehiculoInvalidoException($"La marca '{Marca.Nombre}' no es válida para un tipo de vehículo '{TipoVehiculo.Nombre}'.");
            }

            Placa = placa.ToUpper().Trim(); // Normalizamos la placa
            Modelo = modelo;
            FechaRegistro = DateTime.Now;
        }

        public void AgregarColor(Color color)
        {
            if (color == null)
                throw new VehiculoInvalidoException("El color a agregar no puede ser nulo.");

            if (_colores.Count >= 3)
                throw new VehiculoInvalidoException("La licencia de transito permite registrar maximo tres colores.");

            int ordenEsperado = _colores.Count + 1;
            if (color.Orden == 0)
                color.AsignarOrden(ordenEsperado);

            if (color.Orden != ordenEsperado)
                throw new VehiculoInvalidoException($"El color en posicion {ordenEsperado} debe registrarse antes de continuar.");

            // Regla: Evitar duplicados por nombre (insensible a mayúsculas)
            if (_colores.Any(c => c.Nombre.Equals(color.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new VehiculoInvalidoException($"El vehículo ya tiene asignado el color {color.Nombre}.");

            _colores.Add(color);
        }

        public void CambiarEstado(EstadoVehiculo nuevoEstado)
        {
            // Aquí podrías agregar lógica: ej. No se puede pasar de 'Rentado' a 'Mantenimiento' directamente
            Estado = nuevoEstado ?? throw new VehiculoInvalidoException("El nuevo estado no puede ser nulo.");
        }
    }
}
