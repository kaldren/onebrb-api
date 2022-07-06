namespace Onebrb.Domain.Interfaces
{
    public interface IEntity<T>
    {
        /// <summary>
        /// The Id of a given entity.
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// The IsDeleted property of a given entity.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
