namespace Onebrb.Domain.Exceptions.Comment;
public class CommentCreateException : Exception
{
    public CommentCreateException()
    {
    }

    public CommentCreateException(string message) : base(message)
    {

    }

    public CommentCreateException(string message, Exception inner) : base(message, inner)
    {
    }
}
