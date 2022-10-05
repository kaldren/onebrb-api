using Onebrb.Domain.Interfaces;

namespace Onebrb.Domain.Entities
{
    public abstract class BaseEntity : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the entity id property
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the entity IsDeleted property
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the entity tracking id property
        /// </summary>

        public string TrackId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the entity creation date property
        /// </summary>

        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Gets or sets the entity last update property
        /// </summary>
        public DateTimeOffset LastUpdateDate { get; set; } = DateTimeOffset.UtcNow;
    }
}
