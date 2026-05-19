using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Notificaciones.Queries.GetNotificacionesUsuario
{
    public class GetNotificacionesUsuarioQuery : IRequest<List<NotificacionDto>>
    {
        public required int UsuarioId { get; set; }
    }
}
