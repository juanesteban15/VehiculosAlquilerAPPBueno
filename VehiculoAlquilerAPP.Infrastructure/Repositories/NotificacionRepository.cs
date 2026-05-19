using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class NotificacionRepository : Repository<Notificacion, int>, INotificacionRepository
    {
        public NotificacionRepository(AlquilerDbContext context) : base(context)
        {
        }
    }
}
