namespace Onebrb.Application.Messages.Models;

public class MessageModel
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the content
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the author id
    /// </summary>
    public string AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the author first name
    /// </summary>
    public string AuthorFirstName { get; set; }

    /// <summary>
    /// Gets or sets the author last name
    /// </summary>
    public string AuthorLastName { get; set; }

    /// <summary>
    /// Gets or sets the recipient id
    /// </summary>
    public string RecipientId { get; set; }

    /// <summary>
    /// Gets or sets the recipient first name
    /// </summary>
    public string RecipientFirstName { get; set; }

    /// <summary>
    /// Gets or sets the recipient last name
    /// </summary>
    public string RecipientLastName { get; set; }

    /// <summary>
    /// Gets or sets the date of sending message
    /// </summary>
    public DateTimeOffset Date { get; set; }
}
