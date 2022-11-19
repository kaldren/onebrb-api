namespace Onebrb.API.Models.Responses.Comment
{
    public class CommentsResponseModel
    {
        public int Count { get; set; }

        public IQueryable<CommentResponseModel> Comments { get; set; }
    }
}
