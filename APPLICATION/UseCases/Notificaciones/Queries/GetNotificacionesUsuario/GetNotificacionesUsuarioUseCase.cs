using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Notificaciones.Queries.GetNotificacionesUsuario
{
    public class GetNotificacionesUsuarioUseCase : IRequestHandler<GetNotificacionesUsuarioQuery, List<NotificacionDto>>
    {
        private readonly INotificacionReadService _readService;

        public GetNotificacionesUsuarioUseCase(INotificacionReadService readService)
        {
            _readService = readService;
        }

        public Task<List<NotificacionDto>> Handle(GetNotificacionesUsuarioQuery request)
        {
            return _readService.GetByUsuarioAsync(request.UsuarioId);
        }
    }
}
