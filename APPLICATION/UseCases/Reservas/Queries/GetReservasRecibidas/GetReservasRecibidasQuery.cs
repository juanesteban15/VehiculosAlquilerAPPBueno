using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetReservasRecibidas
{
    public class GetReservasRecibidasQuery : IRequest<List<ReservaResumenDto>>
    {
        public required int DuenioId { get; set; }
    }
}
