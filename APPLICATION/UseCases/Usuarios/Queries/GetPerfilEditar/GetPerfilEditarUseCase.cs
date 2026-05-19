using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilEditar
{
    public class GetPerfilEditarUseCase : IRequestHandler<GetPerfilEditarQuery, PerfilEdicionDto?>
    {
        private readonly IPerfilReadService _readService;

        public GetPerfilEditarUseCase(IPerfilReadService readService)
        {
            _readService = readService;
        }

        public Task<PerfilEdicionDto?> Handle(GetPerfilEditarQuery request)
        {
            return _readService.GetParaEditarAsync(request.UsuarioId);
        }
    }
}
