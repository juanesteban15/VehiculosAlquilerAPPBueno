using VehiculosAlquilerApp.Application.Contracts.Queries.Perfiles;
using VehiculosAlquilerApp.Application.Utilities.Mediator;

namespace VehiculosAlquilerApp.Application.UseCases.Usuarios.Queries.GetPerfilPublico
{
    public class GetPerfilPublicoUseCase : IRequestHandler<GetPerfilPublicoQuery, PerfilPublicoResult>
    {
        private readonly IPerfilReadService _readService;

        public GetPerfilPublicoUseCase(IPerfilReadService readService)
        {
            _readService = readService;
        }

        public Task<PerfilPublicoResult> Handle(GetPerfilPublicoQuery request)
        {
            return _readService.GetPublicoAsync(request.UsuarioId);
        }
    }
}
