using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario,int>
    {
        Task<Usuario?> GetByEmailAsync(string email);


        Task<(List<Usuario> items, int totalCount)> 

            GetPagedListAsync(
            PaginationRequest request,
            string? nombreFilter,
            CancellationToken cancellationToken = default);
    }
}