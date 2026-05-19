using VehiculosAlquilerApp.Application.Utilities.Mediator;


namespace VehiculosAlquilerApp.Application.UseCases.Tarifas.CrearTarifas
{
    public class CrearTarifaCommand : IRequest<int>
    {
        public required string PlacaId { get; set; }
        public required decimal PrecioPorDia { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; } 

    }
}
