using Onebrb.Domain.Interfaces;

namespace Onebrb.Domain.Entities
{
    public abstract class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
