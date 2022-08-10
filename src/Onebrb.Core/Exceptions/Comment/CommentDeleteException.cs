namespace Onebrb.Domain.Exceptions.Comment;

public class CommentDeleteException : Exception
{
    public CommentDeleteException()
    {
    }

    public CommentDeleteException(string message) : base(message)
    {

    }

    public CommentDeleteException(string message, Exception inner) : base(message, inner)
    {
    }
}
