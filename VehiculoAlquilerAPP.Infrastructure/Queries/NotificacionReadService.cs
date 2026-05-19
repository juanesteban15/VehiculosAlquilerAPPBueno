using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Queries.Notificaciones;
using VehiculosAlquilerApp.Infrastructure.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Queries
{
    public class NotificacionReadService : INotificacionReadService
    {
        private readonly AlquilerDbContext _context;

        public NotificacionReadService(AlquilerDbContext context)
        {
            _context = context;
        }

        public Task<List<NotificacionDto>> GetByUsuarioAsync(int usuarioId)
        {
            return _context.Notificaciones
                .Where(n => n.Usuario.Id == usuarioId)
                .OrderByDescending(n => n.FechaCreacion)
                .Select(n => new NotificacionDto
                {
                    Id = n.Id,
                    Titulo = n.Titulo,
                    Mensaje = n.Mensaje,
                    Ruta = n.Ruta,
                    Leida = n.Leida,
                    FechaCreacion = n.FechaCreacion
                })
                .ToListAsync();
        }
    }
}
