using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculoDetalle
{
    public class GetVehiculoDetalleQuery : IRequest<VehiculoDetalleDto?>
    {
        public required string Placa { get; set; }
    }
}
