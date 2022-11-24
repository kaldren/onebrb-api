using Onebrb.Domain.Entities.CountryAggregate;

namespace Onebrb.Domain.Entities.UserAggregate;

public class UserEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the profile first name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the profile last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the profile full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email visibility
    /// </summary>
    public bool IsEmailVisible { get; set; }

    // TODO: Phone validation - until then not showing phones
    ///// <summary>
    ///// Gets or sets the phone number
    ///// </summary>
    //public string? Phone { get; set; }

    ///// <summary>
    ///// Gets or sets the phone visibility
    ///// </summary>
    //public bool? IsPhoneVisible { get; set; }

    /// <summary>
    /// Gets or sets short information about the services the person provides
    /// </summary>
    public string? About { get; set; }

    /// <summary>
    /// Gets or sets the user profile photo
    /// </summary>
    public string? Photo { get; set; }

    /// <summary>
    /// Gets or sets the user profile photo name
    /// </summary>
    public string? PhotoName { get; set; }

    /// <summary>
    /// Gets or sets the country
    /// </summary>
    public string? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the city
    /// </summary>
    public string? CityId { get; set; }

    public virtual City? City { get; set; }

    /// <summary>
    /// Gets or sets the comments
    /// </summary>
    public virtual ICollection<CommentEntity>? Comments { get; set; }

    /// <summary>
    /// Gets or sets the ratings
    /// </summary>
    public virtual ICollection<RatingEntity>? Ratings { get; set; }

    /// <summary>
    /// Gets or sets the occupation id
    /// </summary>
    public string? OccupationId { get; set; }

    /// <summary>
    /// Gets or sets the occupation
    /// </summary>
    public virtual OccupationEntity? Occupation { get; set; }

    /// <summary>
    /// Gets or sets the linkedin profile
    /// </summary>
    public string? LinkedInProfile { get; set; }

    /// <summary>
    /// Gets or sets the facebook profile
    /// </summary>
    public string? FacebookProfile { get; set; }

    /// <summary>
    /// Gets or sets the instagram profile
    /// </summary>
    public string? InstagramProfile { get; set; }
}
