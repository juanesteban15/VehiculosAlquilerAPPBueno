 


namespace VehiculosAlquilerApp.Application.Contracts.Repositories
    {
        public interface IRepository<TEntity, TId>
            where TEntity : class   // Comprueba que debe ser una clase 
        {


            Task<TEntity> CreateAsync(TEntity entity);


            Task UpdateAsync(TEntity entity);
            Task DeleteAsync(TEntity entity);
            Task<TEntity?> GetByIdAsync(TId id);
            Task<IEnumerable<TEntity>> GetListAsync();
        }
    }
