using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Pagination;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : Repository<Usuario, int>, IUsuarioRepository
    {
        private readonly AlquilerDbContext _context;

        public UsuarioRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            string normalizedEmail = email.Trim().ToLower();

            // Include Estado porque Usuario tiene una relación con EstadoUsuario
            return await _context.Usuarios
                .Include(u => u.Estado)
                .Include(u => u.Telefonos)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
            // ToLower() porque los emails no distinguen mayúsculas
        }

        public async Task<(List<Usuario> items, int totalCount)> GetPagedListAsync(
            PaginationRequest request,
            string? nombreFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Usuario> query = _context.Usuarios
                .Include(u => u.Estado)
                .AsQueryable();

            // Filtra por nombre O apellido si viene el filtro
            if (!string.IsNullOrWhiteSpace(nombreFilter))
                query = query.Where(u =>
                    u.Nombre.Contains(nombreFilter.Trim()) ||
                    u.Apellido.Contains(nombreFilter.Trim()));

            int totalCount = await query.CountAsync(cancellationToken);

            List<Usuario> items = await query
                .OrderBy(u => u.Apellido) // Ordena por apellido alfabéticamente
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
