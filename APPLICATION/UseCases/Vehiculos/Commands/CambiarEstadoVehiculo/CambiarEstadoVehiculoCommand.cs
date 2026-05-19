using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CambiarEstadoVehiculo
{
    public class CambiarEstadoVehiculoCommand : IRequest
    {
        public required string Placa { get; set; }
        public required string NuevoEstado { get; set; }
    }
}