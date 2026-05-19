namespace VehiculosAlquilerApp.Application.Contracts.Queries.Reservas
{
    public interface IReservaReadService
    {
        Task<List<ReservaResumenDto>> GetMisReservasAsync(int usuarioId);
        Task<List<ReservaResumenDto>> GetRecibidasAsync(int duenioId);
    }
}
