using Onebrb.Core.Domain;
using Onebrb.Core.Interfaces;

namespace Onebrb.Infrastructure
{
    public class OnebrbGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnebrbDbContext onebrbDbContext;

        public OnebrbGenericRepository(OnebrbDbContext onebrbDbContext)
        {
            this.onebrbDbContext = onebrbDbContext;
        }

        public void Delete(TEntity entity)
        {
            onebrbDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await onebrbDbContext.Set<TEntity>().FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            onebrbDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            onebrbDbContext.Set<TEntity>().Update(entity);
        }
    }
}
