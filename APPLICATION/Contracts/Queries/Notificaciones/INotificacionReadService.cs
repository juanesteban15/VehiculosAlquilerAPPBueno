namespace VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones
{
    public interface INotificacionReadService
    {
        Task<List<NotificacionDto>> GetByUsuarioAsync(int usuarioId);
    }
}
