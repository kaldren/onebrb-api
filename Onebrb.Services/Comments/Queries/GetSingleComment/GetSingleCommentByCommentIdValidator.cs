using FluentValidation;

namespace Onebrb.Application.Comments.Queries.GetSingleComment;

public class GetSingleCommentByCommentIdValidator : AbstractValidator<GetSingleCommentByCommentIdQuery>
{
    public GetSingleCommentByCommentIdValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}
