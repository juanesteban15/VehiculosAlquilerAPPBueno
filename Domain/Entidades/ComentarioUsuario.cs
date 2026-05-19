namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class ComentarioUsuario
    {
        public int Id { get; private set; }
        public Usuario UsuarioEvaluado { get; private set; } = default!;
        public Usuario Autor { get; private set; } = default!;
        public int Calificacion { get; private set; }
        public string Comentario { get; private set; } = default!;
        public DateTime FechaCreacion { get; private set; }

        protected ComentarioUsuario() { }

        public ComentarioUsuario(Usuario usuarioEvaluado, Usuario autor, int calificacion, string comentario)
        {
            UsuarioEvaluado = usuarioEvaluado ?? throw new ArgumentNullException(nameof(usuarioEvaluado));
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));

            if (usuarioEvaluado.Id == autor.Id)
                throw new InvalidOperationException("Un usuario no puede calificarse a sí mismo.");

            if (calificacion < 1 || calificacion > 5)
                throw new ArgumentException("La calificación debe estar entre 1 y 5.", nameof(calificacion));

            if (string.IsNullOrWhiteSpace(comentario))
                throw new ArgumentException("El comentario es obligatorio.", nameof(comentario));

            Calificacion = calificacion;
            Comentario = comentario.Trim();
            FechaCreacion = DateTime.Now;
        }
    }
}
