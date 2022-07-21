using Onebrb.Domain.Interfaces;

namespace Onebrb.Domain.Entities
{
    public abstract class BaseEntity : IEntity<string>
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
