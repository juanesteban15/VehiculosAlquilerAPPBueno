using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Exceptions.Reserva;

namespace VehiculosAlquilerApp.Domain.Entidades
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Vehiculo Vehiculo { get; private set; }
        public Usuario Cliente { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public EstadoReserva Estado { get; private set; }
        public Tarifa Tarifa { get; private set; }
        public MetodoPago MetodoPago { get; private set; }
        public decimal PrecioTotal { get; private set; }
        public string Observaciones { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        public Reserva(
            Vehiculo vehiculo,
            Usuario cliente,
            DateTime fechaInicio,
            DateTime fechaFin,
            EstadoReserva estadoInicial,
            Tarifa tarifa,
            MetodoPago metodoPago,
            string observaciones = null
        )
        {
            // Validaciones de Fechas
            if (fechaInicio.Date < DateTime.Now.Date)
                throw new ReservaInvalidoException("No se puede reservar en una fecha pasada.");

            if (fechaFin <= fechaInicio)
                throw new ReservaInvalidoException("La fecha de fin debe ser posterior a la de inicio.");

            // Validaciones de Nulidad
            Vehiculo = vehiculo ?? throw new ArgumentNullException(nameof(vehiculo));
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Estado = estadoInicial ?? throw new ArgumentNullException(nameof(estadoInicial));
            Tarifa = tarifa ?? throw new ArgumentNullException(nameof(tarifa));
            MetodoPago = metodoPago ?? throw new ArgumentNullException(nameof(metodoPago));

            // Cálculo automático del precio (Lógica de Dominio)
            int dias = (fechaFin - fechaInicio).Days;
            if (dias <= 0) dias = 1; // Mínimo un día de cobro
            PrecioTotal = dias * tarifa.PrecioPorDia;

            Observaciones = observaciones;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaCreacion = DateTime.Now;
        }

        public void Aceptar(Usuario duenio, EstadoReserva estadoConfirmado)
        {
            ValidarGestionPropietario(duenio);

            if (Estado.Nombre != EstadoReserva.Pendiente)
                throw new ReservaInvalidoException("Solo se puede aceptar una reserva que esté pendiente.");

            Estado = estadoConfirmado ?? throw new ArgumentNullException(nameof(estadoConfirmado));
        }

        public void Rechazar(Usuario duenio, EstadoReserva estadoCancelado)
        {
            ValidarGestionPropietario(duenio);

            if (Estado.Nombre != EstadoReserva.Pendiente)
                throw new ReservaInvalidoException("Solo se puede rechazar una reserva que esté pendiente.");

            Estado = estadoCancelado ?? throw new ArgumentNullException(nameof(estadoCancelado));
        }



        public void CambiarEstado(Usuario duenio, EstadoReserva nuevoEstado)
        {
            ValidarGestionPropietario(duenio); 

            if (nuevoEstado == null)
                throw new ArgumentNullException(nameof(nuevoEstado));

            Estado = nuevoEstado;
        }






        // Método privado para evitar repetir código de validación de dueño
        private void ValidarGestionPropietario(Usuario duenio)
        {
            if (Vehiculo.Propietario.Id != duenio.Id)
                throw new ReservaInvalidoException("Acceso denegado: Solo el propietario del vehículo puede gestionar esta reserva.");
        }

        public void FinalizarReserva(EstadoReserva estadoCompletado)
        {
            if (Estado.Nombre != EstadoReserva.Confirmada)
                throw new ReservaInvalidoException("No se puede finalizar una reserva que no haya sido confirmada.");

            Estado = estadoCompletado;
        }
    }
}