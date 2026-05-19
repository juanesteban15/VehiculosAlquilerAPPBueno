namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Notificacion
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; } = default!;
        public string Titulo { get; private set; } = default!;
        public string Mensaje { get; private set; } = default!;
        public string? Ruta { get; private set; }
        public bool Leida { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        protected Notificacion() { }

        public Notificacion(Usuario usuario, string titulo, string mensaje, string? ruta = null)
        {
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            Titulo = string.IsNullOrWhiteSpace(titulo) ? throw new ArgumentException("El titulo es obligatorio.", nameof(titulo)) : titulo.Trim();
            Mensaje = string.IsNullOrWhiteSpace(mensaje) ? throw new ArgumentException("El mensaje es obligatorio.", nameof(mensaje)) : mensaje.Trim();
            Ruta = string.IsNullOrWhiteSpace(ruta) ? null : ruta.Trim();
            FechaCreacion = DateTime.Now;
        }

        public void MarcarComoLeida()
        {
            Leida = true;
        }
    }
}
