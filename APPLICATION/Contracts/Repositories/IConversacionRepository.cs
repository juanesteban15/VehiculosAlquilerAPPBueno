using VehiculosAlquilerApp.Domain.Entidades;

namespace VehiculosAlquilerApp.Application.Contracts.Repositories
{
    public interface IConversacionRepository : IRepository<Conversacion, int>
    {
        Task<Conversacion?> GetByReservaIdAsync(int reservaId);
        Task<Conversacion?> GetByIdConParticipantesAsync(int id);
    }
}
