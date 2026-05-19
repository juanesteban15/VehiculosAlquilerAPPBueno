using VehiculosAlquilerApp.Application.Contracts.Queries.Reservas;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Queries.GetMisReservas
{
    public class GetMisReservasUseCase : IRequestHandler<GetMisReservasQuery, List<ReservaResumenDto>>
    {
        private readonly IReservaReadService _readService;

        public GetMisReservasUseCase(IReservaReadService readService)
        {
            _readService = readService;
        }

        public Task<List<ReservaResumenDto>> Handle(GetMisReservasQuery request)
        {
            return _readService.GetMisReservasAsync(request.UsuarioId);
        }
    }
}
