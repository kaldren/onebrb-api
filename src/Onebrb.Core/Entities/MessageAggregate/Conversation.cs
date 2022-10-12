namespace Onebrb.Domain.Entities.MessageAggregate
{
    public class Conversation : BaseEntity
    {
        public virtual ICollection<Message> Messages { get; set; }
    }
}
