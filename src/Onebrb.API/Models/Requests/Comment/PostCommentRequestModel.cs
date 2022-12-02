using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Onebrb.API.Models.Requests
{
    public class PostCommentRequestModel
    {
        /// <summary>
        /// Gets or sets the content
        /// </summary>

        [Required(ErrorMessage = "Content is required")]
        [CommentContent(AllowUrls: false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author id
        /// </summary>
        [Required(ErrorMessage = "Author is required")]
        public string AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the recipient id
        /// </summary>
        [Required(ErrorMessage = "Recipient is required")]
        public string RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the rating
        /// </summary>
        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }
    }

    internal class CommentContentAttribute : ValidationAttribute
    {
        private bool _allowUrls;

        public CommentContentAttribute(bool AllowUrls)
        {
            _allowUrls = AllowUrls;
        }

        public string GetErrorMessage() =>
            $"The comment must not contain any urls / websites.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (_allowUrls == false)
            {
                var comment = (PostCommentRequestModel)validationContext.ObjectInstance;

                var urlRegex = new Regex(@"((?:(?:http|ftp|ws)s?|sftp):\/\/?)?([^:/\s.#?]+\.[^:/\s#?]+|localhost)(:\d+)?((?:\/\w+)*\/)?([\w\-.]+[^#?\s]+)?([^#]+)?(#[\w-]*)?");

                if (urlRegex.IsMatch(comment.Content))
                {
                    return new ValidationResult(GetErrorMessage());
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
