using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CambiarEstadoReserva
{
    public class CambiarEstadoReservaUseCase : IRequestHandler<CambiarEstadoReservaCommand>
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly IUsuarioRepository _usuarioRepo;    
        private readonly IUnitOfWork _unitOfWork;

        public CambiarEstadoReservaUseCase(
            IReservaRepository reservaRepo,
            IEstadoReservaRepository estadoRepo,
            IUsuarioRepository usuarioRepo,  
            IUnitOfWork unitOfWork)
        {
            _reservaRepo = reservaRepo;
            _estadoRepo = estadoRepo;
            _usuarioRepo = usuarioRepo; 
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CambiarEstadoReservaCommand command)
        {
            var reserva = await _reservaRepo.GetByIdAsync(command.ReservaId)
                ?? throw new BusinessException(
                    $"La reserva con ID {command.ReservaId} no existe.");

            var estado = await _estadoRepo.GetByNombreAsync(command.NuevoEstado)
                ?? throw new BusinessException(
                    $"El estado '{command.NuevoEstado}' no es válido.");



            var usuario = await _usuarioRepo.GetByIdAsync(command.UsuarioId)
                ?? throw new BusinessException(
                    $"El usuario con ID {command.UsuarioId} no existe.");



            reserva.CambiarEstado(usuario,estado);

            try
            {
                await _reservaRepo.UpdateAsync(reserva);
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