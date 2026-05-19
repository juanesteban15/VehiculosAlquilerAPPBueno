namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.GetVehiculosList
{
    public class VehiculoListItemDTO
    {
        public string Placa { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string TipoVehiculo { get; set; } = null!;
        public int Modelo { get; set; }
        public string Estado { get; set; } = null!;
    }
}