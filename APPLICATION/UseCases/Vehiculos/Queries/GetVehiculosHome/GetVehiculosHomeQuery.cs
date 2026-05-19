using VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Queries.GetVehiculosHome
{
    public class GetVehiculosHomeQuery : IRequest<VehiculosHomeResult>
    {
        public VehiculosHomeFilter Filter { get; set; } = new();
    }
}
