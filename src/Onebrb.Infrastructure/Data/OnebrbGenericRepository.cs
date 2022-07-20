using Microsoft.EntityFrameworkCore;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities;
using System.Linq.Expressions;

namespace Onebrb.Infrastructure.Data
{
    public class OnebrbGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnebrbDbContext onebrbDbContext;
        private DbSet<TEntity> dbSet;

        public OnebrbGenericRepository(OnebrbDbContext onebrbDbContext)
        {
            this.onebrbDbContext = onebrbDbContext;
            dbSet = onebrbDbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
            => dbSet.Remove(entity);

        public async Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> func)
            => await dbSet.Where(func).FirstOrDefaultAsync();

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func)
            => await dbSet.Where(func).ToListAsync();

        public async Task<TEntity?> GetAsync(string id)
            => await dbSet.Where(p => p.Id == id).SingleOrDefaultAsync();

        public void Insert(TEntity entity)
            => dbSet.Add(entity);

        public void Update(TEntity entity)
            => dbSet.Update(entity);
    }
}
