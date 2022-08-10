namespace Onebrb.Domain.Exceptions;
public class CommentException : Exception
{
    public CommentException()
    {
    }

    public CommentException(string message) : base(message)
    {

    }

    public CommentException(string message, Exception inner) : base(message, inner)
    {
    }
}
