using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Tarifas.CrearTarifas
{
    public class CrearTarifaUseCase : IRequestHandler<CrearTarifaCommand, int>
    {
        private readonly ITarifaRepository _tarifaRepo;
        private readonly IVehiculoRepository _vehiculoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearTarifaUseCase(
            ITarifaRepository tarifaRepo,
            IVehiculoRepository vehiculoRepo,
            IUnitOfWork unitOfWork)
        {
            _tarifaRepo = tarifaRepo;
            _vehiculoRepo = vehiculoRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CrearTarifaCommand command)
        {
            // 1. Verificar que el vehículo exista
            var vehiculo = await _vehiculoRepo.GetByIdAsync(command.PlacaId)
                ?? throw new BusinessException($"El vehículo con placa {command.PlacaId} no existe.");

            // 2. Crear la entidad de dominio
            var tarifa = new Tarifa(
                vehiculo,
                command.PrecioPorDia,
                command.FechaInicio
            );

            // 3. Persistir y confirmar la transacción
            try
            {
                Tarifa nueva = await _tarifaRepo.CreateAsync(tarifa);
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