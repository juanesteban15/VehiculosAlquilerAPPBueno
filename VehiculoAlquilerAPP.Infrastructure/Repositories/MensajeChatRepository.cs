using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class MensajeChatRepository : Repository<MensajeChat, int>, IMensajeChatRepository
    {
        public MensajeChatRepository(AlquilerDbContext context) : base(context)
        {
        }
    }
}
