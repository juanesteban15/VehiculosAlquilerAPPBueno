using VehiculosAlquilerApp.Domain.Exceptions.Color;

namespace VehiculosAlquilerApp.Domain.ValueObjects
{
    public class Color
    {
        public int Id { get; private set; }
        public int? ColorVehiculoId { get; private set; }
        public string Nombre { get; private set; }
        public int Orden { get; private set; }

        protected Color() { }

        public Color(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ColorInvalidoException("Color requerido");

            Nombre = nombre.Trim().ToUpper();
        }

        public Color(int colorVehiculoId, string nombre, int orden)
        {
            if (colorVehiculoId <= 0)
                throw new ColorInvalidoException("El color debe venir del catalogo oficial.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ColorInvalidoException("Color requerido");

            if (orden < 1 || orden > 3)
                throw new ColorInvalidoException("El orden del color debe estar entre 1 y 3.");

            ColorVehiculoId = colorVehiculoId;
            Nombre = nombre.Trim().ToUpper();
            Orden = orden;
        }

        internal void AsignarOrden(int orden)
        {
            if (orden < 1 || orden > 3)
                throw new ColorInvalidoException("El orden del color debe estar entre 1 y 3.");

            Orden = orden;
        }
    }
}
