using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.RechazarReserva
{
    public class RechazarReservaCommand : IRequest
    {
        public required int ReservaId { get; set; }
        public required int DuenioId { get; set; }
    }
}
