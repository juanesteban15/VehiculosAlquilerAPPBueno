using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Repositories;
using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class ConversacionRepository : Repository<Conversacion, int>, IConversacionRepository
    {
        private readonly AlquilerDbContext _context;

        public ConversacionRepository(AlquilerDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Conversacion?> GetByReservaIdAsync(int reservaId)
        {
            return _context.Conversaciones
                .Include(c => c.Reserva)
                .Include(c => c.Cliente)
                .Include(c => c.Propietario)
                .FirstOrDefaultAsync(c => c.Reserva.Id == reservaId);
        }

        public Task<Conversacion?> GetByIdConParticipantesAsync(int id)
        {
            return _context.Conversaciones
                .Include(c => c.Cliente)
                .Include(c => c.Propietario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
