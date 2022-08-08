using FluentValidation;

namespace Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;

public class GetSingleCommentByCommentIdValidator : AbstractValidator<GetSingleCommentByCommentIdQuery>
{
    public GetSingleCommentByCommentIdValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}
