// ============================================================
// ARCHIVO: UnitOfWorks/EfCoreUnitOfWork.cs
// ============================================================
// Implementa IUnitOfWork usando Entity Framework
// CommitAsync es quien realmente ejecuta el SQL en la base de datos

using VehiculosAlquilerApp.Application.Contracts.Persistence;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.UnitOfWorks
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly AlquilerDbContext _context;

        public EfCoreUnitOfWork(AlquilerDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            // Aquí es donde EF ejecuta todos los INSERT, UPDATE, DELETE pendientes
            // Todo lo que hiciste con CreateAsync, UpdateAsync, DeleteAsync
            // se ejecuta en SQL en este momento
            await _context.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            // En EF no necesitas hacer nada explícito para el rollback
            // Si no llamas SaveChangesAsync, los cambios se descartan solos
            // al final del scope del request HTTP
            return Task.CompletedTask;
        }
    }
}