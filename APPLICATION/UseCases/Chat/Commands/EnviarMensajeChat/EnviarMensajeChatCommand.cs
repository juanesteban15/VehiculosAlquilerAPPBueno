using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Chat.Commands.EnviarMensajeChat
{
    public class EnviarMensajeChatCommand : IRequest
    {
        public required int ConversacionId { get; set; }
        public required int AutorId { get; set; }
        public required string Texto { get; set; }
    }
}
