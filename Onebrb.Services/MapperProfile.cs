using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            #region Profile
            CreateMap<Profile, UserProfileModel>();
            CreateMap<UpdateUserProfileModel, Profile>();
            #endregion

            #region Comment
            CreateMap<Comment, CommentModel>()
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
