using VehiculosAlquilerApp.Domain.Exceptions.Color;

namespace VehiculosAlquilerApp.Domain.ValueObjects
{
    public class Color
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public Color(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ColorInvalidoException("Color requerido");

            Nombre = nombre;
        }
    }
}