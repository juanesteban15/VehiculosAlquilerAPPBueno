namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Conversacion
    {
        public int Id { get; private set; }
        public Reserva Reserva { get; private set; } = default!;
        public Usuario Cliente { get; private set; } = default!;
        public Usuario Propietario { get; private set; } = default!;
        public bool Activa { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        protected Conversacion() { }

        public Conversacion(Reserva reserva, Usuario cliente, Usuario propietario)
        {
            Reserva = reserva ?? throw new ArgumentNullException(nameof(reserva));
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Propietario = propietario ?? throw new ArgumentNullException(nameof(propietario));
            Activa = true;
            FechaCreacion = DateTime.Now;
        }
    }
}
