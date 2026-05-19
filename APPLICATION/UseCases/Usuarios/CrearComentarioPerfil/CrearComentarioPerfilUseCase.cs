using VehiculosAlquilerApp.Application.Contracts.Persistence;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Application.Utilities.Mediator;
using VehiculosAlquilerApp.Domain.Entidades;
using VehiculosAlquilerApp.Domain.Exceptions.Base;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.CrearComentarioPerfil
{
    public class CrearComentarioPerfilUseCase : IRequestHandler<CrearComentarioPerfilCommand>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IComentarioUsuarioRepository _comentarioRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearComentarioPerfilUseCase(
            IUsuarioRepository usuarioRepo,
            IComentarioUsuarioRepository comentarioRepo,
            IUnitOfWork unitOfWork)
        {
            _usuarioRepo = usuarioRepo;
            _comentarioRepo = comentarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CrearComentarioPerfilCommand command)
        {
            Usuario evaluado = await _usuarioRepo.GetByIdAsync(command.UsuarioEvaluadoId)
                ?? throw new BusinessException("El usuario evaluado no existe.");
            Usuario autor = await _usuarioRepo.GetByIdAsync(command.AutorId)
                ?? throw new BusinessException("El usuario autor no existe.");

            try
            {
                await _comentarioRepo.CreateAsync(new ComentarioUsuario(evaluado, autor, command.Calificacion, command.Comentario));
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
