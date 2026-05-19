using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetMisReservas
{
    public class GetMisReservasQuery : IRequest<List<ReservaResumenDto>>
    {
        public required int UsuarioId { get; set; }
    }
}
