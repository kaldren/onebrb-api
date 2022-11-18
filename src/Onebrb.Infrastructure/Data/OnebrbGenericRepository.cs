using Microsoft.EntityFrameworkCore;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities;
using System.Linq.Expressions;

namespace Onebrb.Infrastructure.Data
{
    public class OnebrbGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnebrbDbContext _onebrbDbContext;
        private DbSet<TEntity> _dbSet;

        public OnebrbGenericRepository(OnebrbDbContext onebrbDbContext)
        {
            this._onebrbDbContext = onebrbDbContext;
            _dbSet = onebrbDbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            //If the type we are trying to delete is auditable, then we don't actually delete it but instead set it to be updated with a delete date. 
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                (entity as BaseEntity).DateDeleted = DateTimeOffset.UtcNow;
                (entity as BaseEntity).IsDeleted = true;
                _dbSet.Attach(entity);
                _onebrbDbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> func)
            => await _dbSet.Where(func).FirstOrDefaultAsync();

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func)
            => await _dbSet.Where(func).ToListAsync();

        public async Task<TEntity?> GetAsync(string id)
            => await _dbSet.Where(p => p.Id == id).SingleOrDefaultAsync();

        public void Insert(TEntity entity)
            => _dbSet.Add(entity);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);
    }
}
