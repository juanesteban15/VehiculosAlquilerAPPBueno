
using VehiculosAlquilerApp.Application.Interface;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Catalogo;
using System;

namespace VehiculosAlquilerApp.Application.UseCases.Reservas
{
    public class CrearReserva
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IVehiculoRepository _vehiculoRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMetodoPagoRepository _metodoPagoRepo;
        private readonly IEstadoReservaRepository _estadoRepo;
        private readonly ITarifaRepository _tarifaRepo;

        public CrearReserva(
            IReservaRepository reservaRepo,
            IVehiculoRepository vehiculoRepo,
            IUsuarioRepository usuarioRepo,
            IMetodoPagoRepository metodoPagoRepo,
            IEstadoReservaRepository estadoRepo,
            ITarifaRepository tarifaRepo
        )
        {
            _reservaRepo = reservaRepo;
            _vehiculoRepo = vehiculoRepo;
            _usuarioRepo = usuarioRepo;
            _metodoPagoRepo = metodoPagoRepo;
            _estadoRepo = estadoRepo;
            _tarifaRepo = tarifaRepo;
        }

        public void Ejecutar(
            string placa,
            int usuarioId,
            DateTime fechaInicio,
            DateTime fechaFin,
            int metodoPagoId
        )
        {
            // 1. Recuperar Entidades y Validar Existencia
            var vehiculo = _vehiculoRepo.ObtenerPorPlaca(placa)
                ?? throw new Exception("El vehículo especificado no existe.");

            var usuario = _usuarioRepo.ObtenerPorId(usuarioId)
                ?? throw new Exception("El usuario no existe.");

            var metodoPago = _metodoPagoRepo.ObtenerPorId(metodoPagoId)
                ?? throw new Exception("Método de pago no válido.");

            // 2. Validación de Negocio: ¿El vehículo puede ser reservado?
            if (vehiculo.Estado.Nombre != EstadoVehiculo.Disponible)
                throw new Exception("El vehículo no está disponible para reserva (está rentado o en mantenimiento).");

            // 3. Obtener la Tarifa ACTUAL del vehículo
            var tarifa = _tarifaRepo.ObtenerActivaPorPlaca(placa)
                ?? throw new Exception("Este vehículo no tiene una tarifa configurada.");

            // 4. Obtener el estado inicial del catálogo
            var estadoInicial = _estadoRepo.ObtenerPorNombre(EstadoReserva.Pendiente)
                ?? throw new Exception("Error interno: Estado 'PENDIENTE' no encontrado.");

            // 5. Crear objeto de dominio
            // Nota: Ya definimos en la clase Reserva que el precio se calcula internamente 
            // o se valida, así que pasamos la tarifa.
            var reserva = new Reserva(
                vehiculo,
                usuario,
                fechaInicio,
                fechaFin,
                estadoInicial,
                tarifa,
                metodoPago
            );

            // 6. Persistir
            _reservaRepo.Guardar(reserva);
        }
    }
}