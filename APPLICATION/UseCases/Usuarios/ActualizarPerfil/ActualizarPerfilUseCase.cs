using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Contracts.Storage;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.ActualizarPerfil
{
    public class ActualizarPerfilUseCase : IRequestHandler<ActualizarPerfilCommand>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IDocumentoVerificacionUsuarioRepository _documentoRepo;
        private readonly IArchivoStorageService _storage;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarPerfilUseCase(
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

        public async Task Handle(ActualizarPerfilCommand command)
        {
            Usuario usuario = await _usuarioRepo.GetByIdAsync(command.UsuarioId)
                ?? throw new BusinessException("El usuario no existe.");

            usuario.ActualizarPerfil(command.Pais, command.FechaNacimiento);

            if (command.FotoPerfil is not null)
            {
                ValidarArchivo(command.FotoPerfil, soloImagen: true);
                string rutaFoto = await _storage.GuardarAsync(command.FotoPerfil, Path.Combine("uploads", "perfiles"), usuario.Id.ToString());
                usuario.ActualizarFotoPerfil(rutaFoto);
            }

            await RegistrarDocumentoAsync(usuario, "Documento de identidad", command.DocumentoIdentidad);
            await RegistrarDocumentoAsync(usuario, "Licencia de conduccion", command.LicenciaConduccion);

            try
            {
                await _usuarioRepo.UpdateAsync(usuario);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task RegistrarDocumentoAsync(Usuario usuario, string tipoDocumento, ArchivoSubida? archivo)
        {
            if (archivo is null || archivo.Length == 0)
                return;

            ValidarArchivo(archivo);
            string ruta = await _storage.GuardarAsync(archivo, Path.Combine("uploads", "verificaciones"), usuario.Id.ToString());
            await _documentoRepo.CreateAsync(new DocumentoVerificacionUsuario(usuario, tipoDocumento, archivo.NombreArchivo, ruta));
        }

        private static void ValidarArchivo(ArchivoSubida archivo, bool soloImagen = false)
        {
            string extension = Path.GetExtension(archivo.NombreArchivo).ToLower();
            string[] permitidas = soloImagen ? [".jpg", ".jpeg", ".png"] : [".jpg", ".jpeg", ".png", ".pdf"];
            if (!permitidas.Contains(extension))
                throw new BusinessException(soloImagen ? "La foto debe ser JPG o PNG." : "El documento debe ser JPG, PNG o PDF.");
        }
    }
}
