using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Application
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            #region Profile
            CreateMap<UserEntity, UserProfileModel>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<UpdateUserProfileModel, UserEntity>()

                // Visibility
                //.ForMember(dest => dest.IsPhoneVisible, opt => opt.MapFrom(src => src.IsPhoneVisible ?? default))
                .ForMember(dest => dest.IsEmailVisible, opt => opt.MapFrom(src => src.IsEmailVisible ?? default))

                .ForMember(dest => dest.PhotoName, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Photo) ? null : src.Id))

                .ForPath(dest => dest.Occupation, opt =>
                {
                    opt.Condition(src => string.IsNullOrWhiteSpace(src.Source.OccupationId));
                    opt.Ignore();
                });
            #endregion

            #region Comment
            CreateMap<CommentEntity, CommentModel>()
                .ForMember(
                    dest => dest.AuthorFirstName,
                    opt => opt.MapFrom(src => src.Author.FirstName)
                )
                .ForMember(
                    dest => dest.AuthorLastName,
                    opt => opt.MapFrom(src => src.Author.LastName)
                )
                .ForMember(
                    dest => dest.RecipientFirstName,
                    opt => opt.MapFrom(src => src.Recipient.FirstName)
                )
                .ForMember(
                    dest => dest.RecipientLastName,
                    opt => opt.MapFrom(src => src.Recipient.LastName)
                );
            #endregion Comment
        }
    }
}
