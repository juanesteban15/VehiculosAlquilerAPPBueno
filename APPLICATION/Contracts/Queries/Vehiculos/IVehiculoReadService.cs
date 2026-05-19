namespace VehiculosAlquilerApp.Application.Contracts.Queries.Vehiculos
{
    public interface IVehiculoReadService
    {
        Task<VehiculosHomeResult> GetHomeAsync(VehiculosHomeFilter filter);
        Task<VehiculoDetalleDto?> GetDetalleAsync(string placa);
        Task<CrearVehiculoCatalogosDto> GetCatalogosCrearAsync();
    }
}
