using VehiculosAlquilerApp.Application.Interface;
using VehiculosAlquilerApp.Domain.Entidades;


namespace VehiculosAlquilerApp.Application.UseCases.Tarifas
{
    public class CrearTarifa
    {
        private readonly ITarifaRepository _tarifaRepo;
        private readonly IVehiculoRepository _vehiculoRepo;

        public CrearTarifa(ITarifaRepository tarifarepo, IVehiculoRepository vehiculoRepo)
        {
            _tarifaRepo = tarifarepo;
            _vehiculoRepo = vehiculoRepo;
        }

        public void Ejecutar(
            string placa,        // Eliminamos el ID ya que suele ser autoincremental
            decimal precioPorDia,
            DateTime fechaInicio
        )
        {
            // 1. Validar que el vehículo exista
            var vehiculo = _vehiculoRepo.ObtenerPorPlaca(placa);

            if (vehiculo == null)
                throw new Exception($"No se puede crear la tarifa: El vehículo con placa {placa} no existe.");

            // 2. (Opcional pero recomendado) Desactivar tarifas anteriores
            // var tarifaAnterior = _tarifaRepo.ObtenerActivaPorPlaca(placa);
            // if (tarifaAnterior != null) tarifaAnterior.Desactivar();

            // 3. Crear objeto de dominio (Usa las validaciones que pusimos en la clase Tarifa)
            var tarifa = new Tarifa(
                vehiculo,
                precioPorDia,
                fechaInicio
            );

            // 4. Guardar en la base de datos
            _tarifaRepo.Guardar(tarifa);
        }
    }
}

