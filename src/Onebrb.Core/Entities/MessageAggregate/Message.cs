namespace Onebrb.Domain.Entities.MessageAggregate
{
    public class Message : BaseEntity
    {
        public string AuthorId { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string RecipientId { get; set; }

        public string RecipientFirstName { get; set; }

        public string RecipientLastName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Date { get; set; }

        public bool IsRead { get; set; }
    }
}
