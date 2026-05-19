using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.AceptarReserva
{
    public class AceptarReservaUseCase : IRequestHandler<AceptarReservaCommand>
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly IConversacionRepository _conversacionRepo;
        private readonly INotificacionRepository _notificacionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AceptarReservaUseCase(
            IReservaRepository reservaRepo,
            IUsuarioRepository usuarioRepo,
            IEstadoReservaRepository estadoRepo,
            IConversacionRepository conversacionRepo,
            INotificacionRepository notificacionRepo,
            IUnitOfWork unitOfWork)
        {
            _reservaRepo = reservaRepo;
            _usuarioRepo = usuarioRepo;
            _estadoRepo = estadoRepo;
            _conversacionRepo = conversacionRepo;
            _notificacionRepo = notificacionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AceptarReservaCommand command)
        {
            var reserva = await _reservaRepo.GetByIdConDetalleAsync(command.ReservaId)
                ?? throw new BusinessException(
                    $"La reserva con ID {command.ReservaId} no existe.");

            var duenio = await _usuarioRepo.GetByIdAsync(command.DuenioId)
                ?? throw new BusinessException(
                    $"El usuario con ID {command.DuenioId} no existe.");

            var estadoConfirmado = await _estadoRepo.GetByNombreAsync(EstadoReserva.Confirmada)
                ?? throw new BusinessException(
                    "Error interno: El estado 'Confirmada' no está configurado.");

            reserva.Aceptar(duenio, estadoConfirmado);

            try
            {
                await _reservaRepo.UpdateAsync(reserva);

                Conversacion? conversacion = await _conversacionRepo.GetByReservaIdAsync(reserva.Id);
                if (conversacion is null)
                {
                    conversacion = new Conversacion(reserva, reserva.Cliente, reserva.Vehiculo.Propietario);
                    await _conversacionRepo.CreateAsync(conversacion);
                }

                await _notificacionRepo.CreateAsync(new Notificacion(
                    reserva.Cliente,
                    "Reserva aceptada",
                    $"Tu reserva de {reserva.Vehiculo.Marca.Nombre} fue aceptada. Ya puedes hablar con el dueno.",
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
