using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculosHome
{
    public class GetVehiculosHomeUseCase : IRequestHandler<GetVehiculosHomeQuery, VehiculosHomeResult>
    {
        private readonly IVehiculoReadService _readService;

        public GetVehiculosHomeUseCase(IVehiculoReadService readService)
        {
            _readService = readService;
        }

        public Task<VehiculosHomeResult> Handle(GetVehiculosHomeQuery request)
        {
            return _readService.GetHomeAsync(request.Filter);
        }
    }
}
