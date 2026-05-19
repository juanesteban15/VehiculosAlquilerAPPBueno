using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculoDetalle
{
    public class GetVehiculoDetalleUseCase : IRequestHandler<GetVehiculoDetalleQuery, VehiculoDetalleDto?>
    {
        private readonly IVehiculoReadService _readService;

        public GetVehiculoDetalleUseCase(IVehiculoReadService readService)
        {
            _readService = readService;
        }

        public Task<VehiculoDetalleDto?> Handle(GetVehiculoDetalleQuery request)
        {
            return _readService.GetDetalleAsync(request.Placa);
        }
    }
}
