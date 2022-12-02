using Onebrb.Application;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Onebrb.API.Models.Requests.User;

public class UpdateUserProfileRequestModel
{
    /// <summary>
    /// Gets or sets the profile Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the profile first name
    /// </summary>
    /// 
    [Required]
    [MinLength(ValidationConstants.FirstNameMinLength)]
    [MaxLength(ValidationConstants.FirstNameMaxLength)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the profile last name
    /// </summary>
    /// 
    [Required]
    [MinLength(ValidationConstants.LastNameMinLength)]
    [MaxLength(ValidationConstants.LastNameMaxLength)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets email visibility
    /// </summary>
    public bool IsEmailVisible { get; set; }

    // TODO: Phone validation - until then not showing phones
    ///// <summary>
    ///// Gets or sets the phone number
    ///// </summary>
    //public string? Phone { get; set; }

    ///// <summary>
    ///// Gets or sets phone visibility
    ///// </summary>
    //public bool? IsPhoneVisible { get; set; }

    /// <summary>
    /// Gets or sets short information about the services the person provides
    /// </summary>
    public string? About { get; set; }

    /// <summary>
    /// Gets or sets user profile photo
    /// </summary>
    public string? Photo { get; set; }

    /// <summary>
    /// Gets or sets user's occupation id
    /// </summary>
    public string? OccupationId { get; set; }

    /// <summary>
    /// Gets or sets the linkedin profile
    /// </summary>
    [SocialAccount(SocialPlatform: "LinkedIn")]
    public string? LinkedInProfile { get; set; }

    /// <summary>
    /// Gets or sets the facebook profile
    /// </summary>
    [SocialAccount(SocialPlatform: "Facebook")]
    public string? FacebookProfile { get; set; }

    /// <summary>
    /// Gets or sets the instagram profile
    /// </summary>
    [SocialAccount(SocialPlatform: "Instagram")]
    public string? InstagramProfile { get; set; }
}

internal class SocialAccountAttribute : ValidationAttribute
{
    private string _socialPlatform;

    public SocialAccountAttribute(string SocialPlatform)
    {
        _socialPlatform = SocialPlatform;
    }

    public string GetErrorMessage(string social) =>
        $"Invalid {social} account. Please provide valid account name.";

    public string GetErrorLengthMessage(string account, int min, int max) =>
        $"{account} account length must be between {min} and {max}";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var social = (UpdateUserProfileRequestModel)validationContext.ObjectInstance;
        var urlRegex = new Regex(@"((?:(?:http|ftp|ws)s?|sftp):\/\/?)?([^:/\s.#?]+\.[^:/\s#?]+|localhost)(:\d+)?((?:\/\w+)*\/)?([\w\-.]+[^#?\s]+)?([^#]+)?(#[\w-]*)?");

        if (_socialPlatform == "LinkedIn" && !string.IsNullOrWhiteSpace(social.LinkedInProfile))
        {
            if (social.LinkedInProfile.Trim().Length < ValidationConstants.SocialAccountMinLength || urlRegex.IsMatch(social.LinkedInProfile))
            {
                return new ValidationResult(GetErrorMessage("LinkedIn"));
            }
        }

        if (_socialPlatform == "Facebook" && !string.IsNullOrWhiteSpace(social.FacebookProfile))
        {
            if (social.FacebookProfile.Trim().Length < ValidationConstants.SocialAccountMinLength || urlRegex.IsMatch(social.FacebookProfile))
            {
                return new ValidationResult(GetErrorMessage("Facebook"));
            }
        }

        if (_socialPlatform == "Instagram" && !string.IsNullOrWhiteSpace(social.InstagramProfile))
        {
            if (social.InstagramProfile.Trim().Length < ValidationConstants.SocialAccountMinLength || urlRegex.IsMatch(social.InstagramProfile))
            {
                return new ValidationResult(GetErrorMessage("Instagram"));
            }
        }

        return ValidationResult.Success;
    }
}