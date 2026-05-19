using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetCrearVehiculoCatalogos
{
    public class GetCrearVehiculoCatalogosUseCase : IRequestHandler<GetCrearVehiculoCatalogosQuery, CrearVehiculoCatalogosDto>
    {
        private readonly IVehiculoReadService _readService;

        public GetCrearVehiculoCatalogosUseCase(IVehiculoReadService readService)
        {
            _readService = readService;
        }

        public Task<CrearVehiculoCatalogosDto> Handle(GetCrearVehiculoCatalogosQuery request)
        {
            return _readService.GetCatalogosCrearAsync();
        }
    }
}
