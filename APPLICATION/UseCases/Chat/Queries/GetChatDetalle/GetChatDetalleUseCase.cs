using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Chat.Queries.GetChatDetalle
{
    public class GetChatDetalleUseCase : IRequestHandler<GetChatDetalleQuery, ChatDetalleDto?>
    {
        private readonly IChatReadService _readService;

        public GetChatDetalleUseCase(IChatReadService readService)
        {
            _readService = readService;
        }

        public Task<ChatDetalleDto?> Handle(GetChatDetalleQuery request)
        {
            return _readService.GetDetalleAsync(request.ConversacionId, request.UsuarioId);
        }
    }
}
