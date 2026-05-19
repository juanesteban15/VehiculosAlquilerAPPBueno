using VehiculosAlquilerApp.Application.Contracts.Queries.Chat;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Chat.Queries.GetChatDetalle
{
    public class GetChatDetalleQuery : IRequest<ChatDetalleDto?>
    {
        public required int ConversacionId { get; set; }
        public required int UsuarioId { get; set; }
    }
}
