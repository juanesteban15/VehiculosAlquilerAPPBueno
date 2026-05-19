using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;
using VehiculosAlquilerApp.Domain.ValueObjects;

namespace VehiculosAlquilerApp.Application.UseCases.Vehiculos.Commands.CrearVehiculo
{
    public class CrearVehiculoUseCase : IRequestHandler<CrearVehiculoCommand, string>
    {
        private readonly IVehiculoRepository _repo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEstadoVehiculoRepository _estadoRepo;
        private readonly IMarcaRepository _marcaRepo;
        private readonly ICategoriaRepository _categoriaRepo;
        private readonly ITipoCombustibleRepository _combustibleRepo;
        private readonly ITipoVehiculoRepository _tipoVehiculoRepo;
        private readonly ISistemaTransmisionRepository _transmisionRepo;
        private readonly IColorVehiculoRepository _colorRepo;
        private readonly IVehiculoFotoRepository _fotoRepo;
        private readonly IDocumentoVehiculoArchivoRepository _documentoRepo;
        private readonly IArchivoStorageService _storage;
        private readonly IUnitOfWork _unitOfWork;

        public CrearVehiculoUseCase(
            IVehiculoRepository repo,
            IUsuarioRepository usuarioRepo,
            IEstadoVehiculoRepository estadoRepo,
            IMarcaRepository marcaRepo,
            ICategoriaRepository categoriaRepo,
            ITipoCombustibleRepository combustibleRepo,
            ITipoVehiculoRepository tipoVehiculoRepo,
            ISistemaTransmisionRepository transmisionRepo,
            IColorVehiculoRepository colorRepo,
            IVehiculoFotoRepository fotoRepo,
            IDocumentoVehiculoArchivoRepository documentoRepo,
            IArchivoStorageService storage,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _usuarioRepo = usuarioRepo;
            _estadoRepo = estadoRepo;
            _marcaRepo = marcaRepo;
            _categoriaRepo = categoriaRepo;
            _combustibleRepo = combustibleRepo;
            _tipoVehiculoRepo = tipoVehiculoRepo;
            _transmisionRepo = transmisionRepo;
            _colorRepo = colorRepo;
            _fotoRepo = fotoRepo;
            _documentoRepo = documentoRepo;
            _storage = storage;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CrearVehiculoCommand command)
        {
            EstadoVehiculo estado = command.EstadoVehiculoId > 0
                ? await _estadoRepo.GetByIdAsync(command.EstadoVehiculoId) ?? throw new BusinessException("Estado de vehiculo no encontrado.")
                : await _estadoRepo.GetByNombreAsync(EstadoVehiculo.Disponible) ?? throw new BusinessException("Estado disponible no encontrado.");

            Usuario propietario = await _usuarioRepo.GetByIdAsync(command.PropietarioId)
                ?? throw new BusinessException("El propietario especificado no existe.");
            Marca marca = await _marcaRepo.GetByIdAsync(command.MarcaId)
                ?? throw new BusinessException("Marca no encontrada.");
            Categoria categoria = await _categoriaRepo.GetByIdAsync(command.CategoriaId)
                ?? throw new BusinessException("Categoria no encontrada.");
            TipoCombustible combustible = await _combustibleRepo.GetByIdAsync(command.CombustibleId)
                ?? throw new BusinessException("Tipo de combustible no encontrado.");
            TipoVehiculo tipo = await _tipoVehiculoRepo.GetByIdAsync(command.TipoVehiculoId)
                ?? throw new BusinessException("Tipo de vehiculo no encontrado.");
            SistemaTransmision transmision = await _transmisionRepo.GetByIdAsync(command.TransmisionId)
                ?? throw new BusinessException("Sistema de transmision no encontrado.");

            ValidarCatalogoDelTipo(tipo, marca.TipoVehiculo.Id, "La marca no corresponde al tipo seleccionado.");
            ValidarCatalogoDelTipo(tipo, categoria.TipoVehiculo.Id, "La categoria no corresponde al tipo seleccionado.");
            ValidarCatalogoDelTipo(tipo, combustible.TipoVehiculo.Id, "El combustible no corresponde al tipo seleccionado.");
            ValidarCatalogoDelTipo(tipo, transmision.TipoVehiculo.Id, "La transmision no corresponde al tipo seleccionado.");

            List<Color> colores = await ConstruirColoresAsync(command);

            if (command.Fotos.Count != 5)
                throw new BusinessException("Debes subir exactamente 5 fotos del vehiculo.");

            if (command.TarjetaPropiedad is null || command.Soat is null || command.Tecnomecanica is null)
                throw new BusinessException("Debes subir tarjeta de propiedad, SOAT y tecnomecanica.");

            foreach (ArchivoSubida foto in command.Fotos)
                ValidarArchivo(foto, soloImagen: true);

            ValidarArchivo(command.TarjetaPropiedad);
            ValidarArchivo(command.Soat);
            ValidarArchivo(command.Tecnomecanica);

            var vehiculo = new Vehiculo(
                command.Placa,
                tipo,
                marca,
                categoria,
                combustible,
                transmision,
                command.Modelo,
                propietario,
                estado);

            foreach (Color color in colores)
                vehiculo.AgregarColor(color);

            try
            {
                Vehiculo nuevo = await _repo.CreateAsync(vehiculo);

                for (int i = 0; i < command.Fotos.Count; i++)
                {
                    string ruta = await _storage.GuardarAsync(command.Fotos[i], Path.Combine("uploads", "vehiculos"), nuevo.Placa);
                    await _fotoRepo.CreateAsync(new VehiculoFoto(nuevo, ruta, command.Fotos[i].NombreArchivo, i + 1));
                }

                await GuardarDocumentoAsync(nuevo, "Tarjeta de propiedad", command.TarjetaPropiedad);
                await GuardarDocumentoAsync(nuevo, "SOAT", command.Soat);
                await GuardarDocumentoAsync(nuevo, "Tecnomecanica", command.Tecnomecanica);

                await _unitOfWork.CommitAsync();
                return nuevo.Placa;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Color>> ConstruirColoresAsync(CrearVehiculoCommand command)
        {
            if (command.Colores.Count > 0)
                return command.Colores;

            int[] ids = command.ColorIds.Where(id => id > 0).ToArray();
            if (ids.Length == 0)
                throw new BusinessException("Selecciona al menos un color del vehiculo.");

            if (ids.Distinct().Count() != ids.Length)
                throw new BusinessException("No puedes repetir colores en la licencia de transito.");

            List<ColorVehiculo> coloresCatalogo = await _colorRepo.GetActivosPorIdsAsync(ids);
            if (coloresCatalogo.Count != ids.Length)
                throw new BusinessException("Todos los colores deben existir y estar activos.");

            return ids
                .Select((id, index) =>
                {
                    ColorVehiculo color = coloresCatalogo.First(c => c.Id == id);
                    return new Color(color.Id, color.Nombre, index + 1);
                })
                .ToList();
        }

        private static void ValidarCatalogoDelTipo(TipoVehiculo tipo, int tipoCatalogoId, string mensaje)
        {
            if (tipoCatalogoId != tipo.Id)
                throw new BusinessException(mensaje);
        }

        private async Task GuardarDocumentoAsync(Vehiculo vehiculo, string tipoDocumento, ArchivoSubida archivo)
        {
            string ruta = await _storage.GuardarAsync(archivo, Path.Combine("uploads", "documentos-vehiculos"), vehiculo.Placa);
            await _documentoRepo.CreateAsync(new DocumentoVehiculoArchivo(vehiculo, tipoDocumento, archivo.NombreArchivo, ruta));
        }

        private static void ValidarArchivo(ArchivoSubida archivo, bool soloImagen = false)
        {
            if (archivo.Length <= 0)
                throw new BusinessException("El archivo esta vacio.");

            string extension = Path.GetExtension(archivo.NombreArchivo).ToLower();
            string[] permitidas = soloImagen ? [".jpg", ".jpeg", ".png"] : [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
                throw new BusinessException(soloImagen ? "Las fotos deben ser JPG o PNG." : "El archivo debe ser JPG, PNG o PDF.");
        }
    }
}
