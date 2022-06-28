using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Domain;
using Onebrb.Core.Interfaces;
using System.Linq.Expressions;

namespace Onebrb.Infrastructure
{
    public class OnebrbGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnebrbDbContext onebrbDbContext;
        private DbSet<TEntity> dbSet;

        public OnebrbGenericRepository()
        {

        }

        public OnebrbGenericRepository(OnebrbDbContext onebrbDbContext)
        {
            this.onebrbDbContext = onebrbDbContext;
            this.dbSet = onebrbDbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> func)
        {
            return await this.dbSet.Where(func).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
