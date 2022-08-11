namespace Onebrb.Domain.Exceptions.Comment;

public class CommentUpdateException : Exception
{
    public CommentUpdateException()
    {
    }

    public CommentUpdateException(string message) : base(message)
    {

    }

    public CommentUpdateException(string message, Exception inner) : base(message, inner)
    {
    }
}
