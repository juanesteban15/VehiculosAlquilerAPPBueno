using VehiculosAlquilerApp.Application.Interface;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos
{
    public class CrearVehiculo
    {
        private readonly IVehiculoRepository _repo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEstadoVehiculoRepository _estadoVEHRepo;
        private readonly IMarcaRepository _marcaRepo;
        private readonly ICategoriaRepository _categoriaRepo;
        private readonly ITipoCombustibleRepository _combustibleRepo;
        private readonly ITipoVehiculoRepository _tipoVehiculoRepo;
        private readonly ISistemaTransmisionRepository _transmisionRepo; // 1. Agregado

        public CrearVehiculo(
            IVehiculoRepository repo,
            IEstadoVehiculoRepository estadoVEHRepo,
            IUsuarioRepository usuarioRepository,
            IMarcaRepository marcaRepository,
            ICategoriaRepository categoriaRepository,
            ITipoCombustibleRepository combustibleRepository,
            ITipoVehiculoRepository tipoVehiculoRepository,
            ISistemaTransmisionRepository transmisionRepo) // 2. Inyectado
        {
            _repo = repo;
            _estadoVEHRepo = estadoVEHRepo;
            _usuarioRepo = usuarioRepository;
            _marcaRepo = marcaRepository;
            _categoriaRepo = categoriaRepository;
            _combustibleRepo = combustibleRepository;
            _tipoVehiculoRepo = tipoVehiculoRepository;
            _transmisionRepo = transmisionRepo;
        }

        public void Ejecutar(
            string placa,
            int tipoVehiculoId,
            int marcaId,
            int categoriaId,
            int combustibleId,
            int transmisionId,
            int modelo,
            int propietarioId,
            int estadoVehiculoId,
            List<Color> colores
        )
        {
            // 3. Recuperar y Validar Entidades (Fail-fast principle)
            var estado = _estadoVEHRepo.ObtenerPorId(estadoVehiculoId)
                ?? throw new Exception("Estado de vehículo no encontrado.");

            var propietario = _usuarioRepo.ObtenerPorId(propietarioId)
                ?? throw new Exception("El propietario especificado no existe.");

            var marca = _marcaRepo.ObtenerPorId(marcaId)
                ?? throw new Exception("Marca no encontrada.");

            var categoria = _categoriaRepo.ObtenerPorId(categoriaId)
                ?? throw new Exception("Categoría no encontrada.");

            var combustible = _combustibleRepo.ObtenerPorId(combustibleId)
                ?? throw new Exception("Tipo de combustible no encontrado.");

            var tipo = _tipoVehiculoRepo.ObtenerPorId(tipoVehiculoId)
                ?? throw new Exception("Tipo de vehículo no encontrado.");

            var transmision = _transmisionRepo.ObtenerPorId(transmisionId)
                ?? throw new Exception("Sistema de transmisión no encontrado.");

            // 4. Crear la Entidad de Negocio
            var vehiculo = new Vehiculo(
                placa,
                tipo,
                marca,
                categoria,
                combustible,
                transmision,
                modelo,
                propietario,
                estado
            );

            // 5. Agregar colores si existen
            if (colores != null)
            {
                foreach (var color in colores)
                {
                    vehiculo.AgregarColor(color);
                }
            }

            // 6. Persistir
            _repo.Guardar(vehiculo);
        }
    }
}