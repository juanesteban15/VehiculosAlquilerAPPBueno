namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class MensajeChat
    {
        public int Id { get; private set; }
        public Conversacion Conversacion { get; private set; } = default!;
        public Usuario Autor { get; private set; } = default!;
        public string Texto { get; private set; } = default!;
        public DateTime FechaEnvio { get; private set; }

        protected MensajeChat() { }

        public MensajeChat(Conversacion conversacion, Usuario autor, string texto)
        {
            Conversacion = conversacion ?? throw new ArgumentNullException(nameof(conversacion));
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            Texto = string.IsNullOrWhiteSpace(texto) ? throw new ArgumentException("El mensaje no puede estar vacio.", nameof(texto)) : texto.Trim();
            FechaEnvio = DateTime.Now;
        }
    }
}
