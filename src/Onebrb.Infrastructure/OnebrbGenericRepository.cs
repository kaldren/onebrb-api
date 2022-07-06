using Microsoft.EntityFrameworkCore;
using Onebrb.Domain.Entities;
using Onebrb.Domain.Interfaces;
using System.Linq.Expressions;

namespace Onebrb.Infrastructure
{
    public class OnebrbGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnebrbDbContext onebrbDbContext;
        private DbSet<TEntity> dbSet;

        public OnebrbGenericRepository(OnebrbDbContext onebrbDbContext)
        {
            this.onebrbDbContext = onebrbDbContext;
            this.dbSet = onebrbDbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
            => this.dbSet.Remove(entity);

        public async Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> func)
            => await this.dbSet.Where(func).FirstOrDefaultAsync();

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func)
            => await this.dbSet.Where(func).ToListAsync();

        public async Task<TEntity?> GetAsync(long id)
            => await this.dbSet.Where(p => p.Id == id).SingleOrDefaultAsync();

        public void Insert(TEntity entity)
            => this.dbSet.Add(entity);

        public void Update(TEntity entity)
            => this.dbSet.Update(entity);
    }
}
