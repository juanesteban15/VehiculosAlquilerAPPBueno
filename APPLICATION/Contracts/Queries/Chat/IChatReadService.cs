namespace VehiculosAlquilerApp.Application.Contracts.Queries.Chat
{
    public interface IChatReadService
    {
        Task<ChatDetalleDto?> GetDetalleAsync(int conversacionId, int usuarioId);
    }
}
