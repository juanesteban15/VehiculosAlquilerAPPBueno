using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CambiarEstadoVehiculo
{
    public class CambiarEstadoVehiculoUseCase : IRequestHandler<CambiarEstadoVehiculoCommand>
    {
        private readonly IVehiculoRepository _repo;
        private readonly IEstadoVehiculoRepository _estadoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CambiarEstadoVehiculoUseCase(
            IVehiculoRepository repo,
            IEstadoVehiculoRepository estadoRepo,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _estadoRepo = estadoRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CambiarEstadoVehiculoCommand command)
        {
            var vehiculo = await _repo.GetByIdAsync(command.Placa)
                ?? throw new BusinessException(
                    $"El vehículo con placa {command.Placa} no existe.");

            var estado = await _estadoRepo.GetByNombreAsync(command.NuevoEstado)
                ?? throw new BusinessException(
                    $"El estado '{command.NuevoEstado}' no es válido.");

            vehiculo.CambiarEstado(estado);

            try
            {
                await _repo.UpdateAsync(vehiculo);
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