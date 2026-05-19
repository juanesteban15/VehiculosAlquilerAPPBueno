namespace VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles
{
    public interface IPerfilReadService
    {
        Task<PerfilEdicionDto?> GetParaEditarAsync(int usuarioId);
        Task<PerfilPublicoResult> GetPublicoAsync(int usuarioId);
    }
}
