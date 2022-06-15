using Onebrb.Core.Interfaces;

namespace Onebrb.Core.Domain
{
    public abstract class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
