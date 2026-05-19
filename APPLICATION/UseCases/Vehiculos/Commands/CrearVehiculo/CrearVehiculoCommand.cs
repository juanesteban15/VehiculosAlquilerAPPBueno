using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CrearVehiculo
{
    public class CrearVehiculoCommand : IRequest<string>
    {
        public required string Placa { get; set; }
        public required int PropietarioId { get; set; }
        public required int TipoVehiculoId { get; set; }
        public required int MarcaId { get; set; }
        public required int CategoriaId { get; set; }
        public required int CombustibleId { get; set; }
        public required int TransmisionId { get; set; }
        public required int Modelo { get; set; }
        public int EstadoVehiculoId { get; set; }
        public List<int> ColorIds { get; set; } = [];
        public List<Color> Colores { get; set; } = [];
        public List<ArchivoSubida> Fotos { get; set; } = [];
        public ArchivoSubida? TarjetaPropiedad { get; set; }
        public ArchivoSubida? Soat { get; set; }
        public ArchivoSubida? Tecnomecanica { get; set; }
    }
}
