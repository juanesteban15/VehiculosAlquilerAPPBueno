using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Security;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Catalogo;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearUsuario
{
    public class CrearUsuarioUseCase : IRequestHandler<CrearUsuarioCommand, int>
    {
        private readonly IUsuarioRepository _repo;
        private readonly IEstadoUsuarioRepository _estadoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearUsuarioUseCase(
            IUsuarioRepository repo,
            IEstadoUsuarioRepository estadoRepo,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _estadoRepo = estadoRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CrearUsuarioCommand command)
        {
            // 1. Verificar que el email no esté registrado ya
            var usuarioExistente = await _repo.GetByEmailAsync(command.Email);
            if (usuarioExistente is not null)
                throw new BusinessException("Ya existe un usuario con ese email.");

            // 2. Obtener el estado inicial del catálogo
            var estado = await _estadoRepo.GetByNombreAsync(EstadoUsuario.Sinverificar)
                ?? throw new BusinessException("El estado inicial no existe en el sistema.");

            // 3. Crear la entidad — aquí se disparan las validaciones del dominio
            var usuario = new Usuario(
                command.Nombre,
                command.Apellido,
                command.Email,
                PasswordHasher.Hash(command.Password),
                command.Pais,
                command.FechaNacimiento,
                estado,
                command.TipoCuenta,
                command.NombreEmpresa,
                command.NitEmpresa);

            // 4. Agregar teléfonos si vienen en el command
            foreach (var tel in command.Telefonos)
                usuario.AgregarTelefono(tel.Numero, tel.Tipo);

            // 5. Persistir y confirmar la transacción
            try
            {
                Usuario nuevo = await _repo.CreateAsync(usuario);
                await _unitOfWork.CommitAsync();
                return nuevo.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
