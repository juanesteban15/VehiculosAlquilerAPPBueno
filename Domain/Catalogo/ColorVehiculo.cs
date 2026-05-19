namespace VehiculosAlquilerApp.Domain.Catalogo
{
    public class ColorVehiculo
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = default!;
        public bool Activo { get; private set; }

        protected ColorVehiculo() { }

        public ColorVehiculo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del color es requerido.", nameof(nombre));

            Nombre = nombre.Trim().ToUpper();
            Activo = true;
        }

        public void Activar() => Activo = true;
        public void Desactivar() => Activo = false;
    }
}
