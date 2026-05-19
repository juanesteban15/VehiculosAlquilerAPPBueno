using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.RegistrarDocumentoUsuario
{
    public class RegistrarDocumentoUsuarioUseCase : IRequestHandler<RegistrarDocumentoUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IDocumentoVerificacionUsuarioRepository _documentoRepo;
        private readonly IArchivoStorageService _storage;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarDocumentoUsuarioUseCase(
            IUsuarioRepository usuarioRepo,
            IDocumentoVerificacionUsuarioRepository documentoRepo,
            IArchivoStorageService storage,
            IUnitOfWork unitOfWork)
        {
            _usuarioRepo = usuarioRepo;
            _documentoRepo = documentoRepo;
            _storage = storage;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegistrarDocumentoUsuarioCommand command)
        {
            Usuario usuario = await _usuarioRepo.GetByIdAsync(command.UsuarioId)
                ?? throw new BusinessException("El usuario no existe.");

            ValidarArchivo(command.Archivo);
            string ruta = await _storage.GuardarAsync(command.Archivo, Path.Combine("uploads", "verificaciones"), usuario.Id.ToString());

            try
            {
                await _documentoRepo.CreateAsync(new DocumentoVerificacionUsuario(usuario, command.TipoDocumento, command.Archivo.NombreArchivo, ruta));
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private static void ValidarArchivo(ArchivoSubida archivo)
        {
            if (archivo.Length <= 0)
                throw new BusinessException("El archivo esta vacio.");

            string extension = Path.GetExtension(archivo.NombreArchivo).ToLower();
            string[] permitidas = [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
                throw new BusinessException("El documento debe ser JPG, PNG o PDF.");
        }
    }
}
