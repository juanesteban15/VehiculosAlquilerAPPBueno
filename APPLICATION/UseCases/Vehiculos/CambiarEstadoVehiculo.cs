using VehiculosAlquilerApp.Application.Interface;
using System;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos
{
    public class CambiarEstadoVehiculo
    {
        private readonly IVehiculoRepository _repo;
        private readonly IEstadoVehiculoRepository _estadoVEHRepo;

        public CambiarEstadoVehiculo(
            IVehiculoRepository repo,
            IEstadoVehiculoRepository estadoVEHRepo
        )
        {
            _repo = repo;
            _estadoVEHRepo = estadoVEHRepo;
        }

        public void Ejecutar(string placa, string nombreNuevoEstado)
        {
            // 1. Buscar el vehículo por su identificador único (Placa)
            var vehiculo = _repo.ObtenerPorPlaca(placa);

            if (vehiculo == null)
                throw new Exception($"El vehículo con placa {placa} no existe en el sistema.");

            // 2. Recuperar el objeto de estado desde el catálogo
            var estado = _estadoVEHRepo.ObtenerPorNombre(nombreNuevoEstado);

            if (estado == null)
                throw new Exception($"El estado '{nombreNuevoEstado}' no es un estado de catálogo válido.");

            // 3. Ejecutar la lógica de cambio de estado en la entidad (Dominio)
            // Aquí es donde se aplican las reglas de validación que pusimos en la clase Vehiculo
            vehiculo.CambiarEstado(estado);

            // 4. Persistir los cambios
            _repo.Actualizar(vehiculo);
        }
    }
}