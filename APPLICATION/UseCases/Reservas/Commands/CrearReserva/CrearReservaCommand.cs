using VehiculosAlquilerApp.Application.Utilities.Mediator;


namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CrearReserva
{
    public class CrearReservaCommand : IRequest<int>
    {
        public required string Placa { get; set; }
        public required int UsuarioId { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required int MetodoPagoId { get; set; }
    }
}
