namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class EstadoUsuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public const string Verificado = "Verificado";
        public const string Betado = "Betado";
        public const string Sinverificar = "Sinverificar";
        public const string NoDisponible = "NoDisponible";

        public EstadoUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del estado es requerido.", nameof(nombre));

            Nombre = nombre.ToUpper().Trim();
        }
    }
}