using Microsoft.EntityFrameworkCore;
using VehiculosAlquilerApp.Application.Contracts.Repositories;

namespace VehiculosAlquilerApp.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly AlquilerDbContext _context;

        public Repository(AlquilerDbContext context)
        {
            _context = context;
        }

        public virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Add(entity);
            return Task.FromResult(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            return Task.FromResult(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return Task.FromResult(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }
}
