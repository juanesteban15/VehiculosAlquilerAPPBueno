namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class MetodoPago
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        // Constantes sugeridas para evitar "Magic Strings" en la app
        public const string Efectivo = "EFECTIVO";
        public const string TarjetaCredito = "TARJETA_CREDITO";
        public const string Transferencia = "TRANSFERENCIA";
        public const string Pse = "PSE";

        // Constructor para Entity Framework
        protected MetodoPago() { }

        public MetodoPago(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del método de pago es requerido.", nameof(nombre));

            Nombre = nombre.Trim().ToUpper();
        }
    }
}
