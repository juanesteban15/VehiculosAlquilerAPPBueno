namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class TipoVehiculo
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public bool Activo { get; private set; }

        // Constructor para Entity Framework
        protected TipoVehiculo() { }

        public TipoVehiculo(string codigo, string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código es requerido.", nameof(codigo));

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es requerido.", nameof(nombre));

            Codigo = codigo.Trim().ToUpper();
            Nombre = nombre.Trim();
            Descripcion = descripcion;
            Activo = true;
        }

        public void Desactivar() => Activo = false;
        public void Activar() => Activo = true;
    }
}