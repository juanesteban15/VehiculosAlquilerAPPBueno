// ARCHIVO 2 — ReservaListItemDTO.cs
namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.GetReservasList
{
    public class ReservaListItemDTO
    {
        public int Id { get; set; }
        public string PlacaVehiculo { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = null!;
        public decimal PrecioTotal { get; set; }


    }
}