using Onebrb.Core.Domain;
using System.Linq.Expressions;

namespace Onebrb.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets a single entity.
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="TEntity" /> entity, or null if no entity is found.
        /// </returns>
        Task<TEntity?> GetAsync(long id);

        /// <summary>
        /// Gets a single entity.
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="TEntity" /> entity, or null if no entity is found.
        /// </returns>
        Task<TEntity?> GetSingleOrDefault(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Gets a collection of an entity.
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>
        /// Asynchronously returns a collection of <typeparamref name="TEntity" /> entity, or null if not found.
        /// </returns>
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// Inserts a single <typeparamref name="TEntity" /> entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> entity to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates a single <typeparamref name="TEntity" /> entity.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> entity to delete.</param>
        void Delete(TEntity entity);
    }
}
