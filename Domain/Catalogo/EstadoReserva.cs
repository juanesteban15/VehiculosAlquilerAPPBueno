namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class EstadoReserva
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public const string Pendiente = "PENDIENTE";
        public const string Confirmada = "CONFIRMADA";
        public const string Completada = "COMPLETADA"; // Corregido a mayúsculas
        public const string Cancelada = "CANCELADA";

        protected EstadoReserva() { } // Para EF

        public EstadoReserva(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del estado es requerido.", nameof(nombre));

            Nombre = nombre.Trim().ToUpper();
        }
    }
}