using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas.Commands.CrearReserva
{
    public class CrearReservaUseCase : IRequestHandler<CrearReservaCommand, int>
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IVehiculoRepository _vehiculoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMetodoPagoRepository _metodoPagoRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly ITarifaRepository _tarifaRepo;
        private readonly INotificacionRepository _notificacionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearReservaUseCase(
            IReservaRepository reservaRepo,
            IVehiculoRepository vehiculoRepo,
            IUsuarioRepository usuarioRepo,
            IMetodoPagoRepository metodoPagoRepo,
            IEstadoReservaRepository estadoRepo,
            ITarifaRepository tarifaRepo,
            INotificacionRepository notificacionRepo,
            IUnitOfWork unitOfWork)
        {
            _reservaRepo = reservaRepo;
            _vehiculoRepo = vehiculoRepo;
            _usuarioRepo = usuarioRepo;
            _metodoPagoRepo = metodoPagoRepo;
            _estadoRepo = estadoRepo;
            _tarifaRepo = tarifaRepo;
            _notificacionRepo = notificacionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CrearReservaCommand command)
        {
            // 1. Verificar que el vehículo exista
            var vehiculo = await _vehiculoRepo.GetByPlacaAsync(command.Placa)
                ?? throw new BusinessException("El vehículo especificado no existe.");

            // 2. Verificar que el vehículo esté disponible
            if (vehiculo.Estado.Nombre != EstadoVehiculo.Disponible)
                throw new BusinessException("El vehículo no está disponible para reserva.");

            // 3. Verificar que el usuario exista
            var usuario = await _usuarioRepo.GetByIdAsync(command.UsuarioId)
                ?? throw new BusinessException("El usuario no existe.");

            if (vehiculo.Propietario.Id == usuario.Id)
                throw new BusinessException("No puedes reservar un vehiculo publicado por ti.");

            // 4. Verificar que el método de pago exista
            var metodoPago = await _metodoPagoRepo.GetByIdAsync(command.MetodoPagoId)
                ?? throw new BusinessException("El método de pago no es válido.");

            if (metodoPago.Nombre != MetodoPago.TarjetaCredito && metodoPago.Nombre != MetodoPago.Pse)
                throw new BusinessException("Por ahora solo se permite Tarjeta o PSE.");

            // 5. Obtener la tarifa activa del vehículo
            var tarifa = await _tarifaRepo.GetActivaPorPlacaAsync(command.Placa)
                ?? throw new BusinessException("Este vehículo no tiene una tarifa configurada.");

            // 6. Obtener el estado inicial de la reserva
            var estadoInicial = await _estadoRepo.GetByNombreAsync(EstadoReserva.Pendiente)
                ?? throw new BusinessException("Error interno: Estado 'Pendiente' no encontrado.");

            // 7. Crear la entidad de dominio
            var reserva = new Reserva(
                vehiculo,
                usuario,
                command.FechaInicio,
                command.FechaFin,
                estadoInicial,
                tarifa,
                metodoPago);

            // 8. Persistir y confirmar la transacción
            try
            {
                Reserva nueva = await _reservaRepo.CreateAsync(reserva);
                await _notificacionRepo.CreateAsync(new Notificacion(
                    vehiculo.Propietario,
                    "Nueva solicitud de reserva",
                    $"{usuario.Nombre} quiere reservar {vehiculo.Marca.Nombre} del {command.FechaInicio:dd/MM/yyyy} al {command.FechaFin:dd/MM/yyyy}.",
                    "/Reservas/Recibidas"));

                await _unitOfWork.CommitAsync();
                return nueva.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
