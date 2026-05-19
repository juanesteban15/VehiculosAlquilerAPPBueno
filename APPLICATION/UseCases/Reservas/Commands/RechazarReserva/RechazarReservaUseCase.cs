using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.RechazarReserva
{
    public class RechazarReservaUseCase : IRequestHandler<RechazarReservaCommand>
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly INotificacionRepository _notificacionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RechazarReservaUseCase(
            IReservaRepository reservaRepo,
            IUsuarioRepository usuarioRepo,
            IEstadoReservaRepository estadoRepo,
            INotificacionRepository notificacionRepo,
            IUnitOfWork unitOfWork)
        {
            _reservaRepo = reservaRepo;
            _usuarioRepo = usuarioRepo;
            _estadoRepo = estadoRepo;
            _notificacionRepo = notificacionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RechazarReservaCommand command)
        {
            var reserva = await _reservaRepo.GetByIdConDetalleAsync(command.ReservaId)
                ?? throw new BusinessException($"La reserva con ID {command.ReservaId} no existe.");

            var duenio = await _usuarioRepo.GetByIdAsync(command.DuenioId)
                ?? throw new BusinessException($"El usuario con ID {command.DuenioId} no existe.");

            var estadoCancelado = await _estadoRepo.GetByNombreAsync(EstadoReserva.Cancelada)
                ?? throw new BusinessException("Error interno: El estado 'Cancelada' no esta configurado.");

            reserva.Rechazar(duenio, estadoCancelado);

            try
            {
                await _reservaRepo.UpdateAsync(reserva);
                await _notificacionRepo.CreateAsync(new Notificacion(
                    reserva.Cliente,
                    "Reserva rechazada",
                    $"Tu reserva de {reserva.Vehiculo.Marca.Nombre} fue rechazada por el dueno.",
                    "/Reservas/MisReservas"));

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
