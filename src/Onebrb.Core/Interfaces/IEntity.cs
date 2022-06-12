namespace Onebrb.Core.Interfaces
{
    public interface IEntity<T>
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        T Id { get; set; }
    }
}
