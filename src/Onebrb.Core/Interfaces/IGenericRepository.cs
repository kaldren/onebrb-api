using Onebrb.Core.Domain;

namespace Onebrb.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets a single entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        TEntity GetByIdAsync(int id);

        /// <summary>
        /// Inserts a single entity
        /// </summary>
        /// <param name="id">Entity</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// </returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Updates a single entity
        /// </summary>
        /// <param name="id">Entity</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// </returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <param name="id">Entity</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// </returns>
        Task DeleteAsync(TEntity entity);
    }
}
