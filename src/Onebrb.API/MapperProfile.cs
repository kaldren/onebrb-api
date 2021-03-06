using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Responses;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.API
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Comment, CommentResponseModel>()
                .ForMember(
                    dest => dest.AuthorFirstName,
                    opt => opt.MapFrom(src => src.Author.FirstName)
                )
                .ForMember(
                    dest => dest.AuthorLastName,
                    opt => opt.MapFrom(src => src.Author.LastName)
                );

            #region Profile
            CreateMap<Profile, ActivatedUserProfileResponseModel>();
            CreateMap<Profile, UserProfileModel>();
            CreateMap<UserProfileModel, ActivatedUserProfileResponseModel>();
            CreateMap<UserProfileModel, UserProfileResponseModel>();
            CreateMap<UpdateUserProfileRequestModel, UpdateUserProfileModel>();
            #endregion Profile
        }
    }
}
