using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Chat.Commands.EnviarMensajeChat
{
    public class EnviarMensajeChatUseCase : IRequestHandler<EnviarMensajeChatCommand>
    {
        private readonly IConversacionRepository _conversacionRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMensajeChatRepository _mensajeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EnviarMensajeChatUseCase(
            IConversacionRepository conversacionRepo,
            IUsuarioRepository usuarioRepo,
            IMensajeChatRepository mensajeRepo,
            IUnitOfWork unitOfWork)
        {
            _conversacionRepo = conversacionRepo;
            _usuarioRepo = usuarioRepo;
            _mensajeRepo = mensajeRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EnviarMensajeChatCommand command)
        {
            Conversacion conversacion = await _conversacionRepo.GetByIdConParticipantesAsync(command.ConversacionId)
                ?? throw new BusinessException("La conversacion no existe.");

            if (conversacion.Cliente.Id != command.AutorId && conversacion.Propietario.Id != command.AutorId)
                throw new BusinessException("No tienes permiso para escribir en este chat.");

            Usuario autor = await _usuarioRepo.GetByIdAsync(command.AutorId)
                ?? throw new BusinessException("El usuario no existe.");

            try
            {
                await _mensajeRepo.CreateAsync(new MensajeChat(conversacion, autor, command.Texto));
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
