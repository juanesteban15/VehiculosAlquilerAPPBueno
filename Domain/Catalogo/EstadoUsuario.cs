namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class EstadoUsuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public const string Disponible = "DISPONIBLE";
        public const string Rentado = "RENTADO";
        public const string Mantenimiento = "MANTENIMIENTO";
        public const string NoDisponible = "NO_DISPONIBLE";

        public EstadoUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del estado es requerido.", nameof(nombre));

            Nombre = nombre.ToUpper().Trim();
        }
    }
}