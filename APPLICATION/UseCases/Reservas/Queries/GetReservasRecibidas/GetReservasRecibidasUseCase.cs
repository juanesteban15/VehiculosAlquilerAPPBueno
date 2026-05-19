using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetReservasRecibidas
{
    public class GetReservasRecibidasUseCase : IRequestHandler<GetReservasRecibidasQuery, List<ReservaResumenDto>>
    {
        private readonly IReservaReadService _readService;

        public GetReservasRecibidasUseCase(IReservaReadService readService)
        {
            _readService = readService;
        }

        public Task<List<ReservaResumenDto>> Handle(GetReservasRecibidasQuery request)
        {
            return _readService.GetRecibidasAsync(request.DuenioId);
        }
    }
}
