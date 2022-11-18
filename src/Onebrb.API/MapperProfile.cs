using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Requests.Comment;
using Onebrb.API.Models.Responses.Comment;
using Onebrb.API.Models.Responses.Occupation;
using Onebrb.API.Models.Responses.Profile;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Occupations.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.API
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
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
                    dest => dest.RatingValue,
                    opt => opt.MapFrom(src => src.Rating.Value)
                );

            CreateMap<CommentModel, CommentResponseModel>();

            CreateMap<PostCommentRequestModel, PostCommentModel>();
            CreateMap<PatchCommentRequestModel, PatchCommentModel>();
            #endregion Comment

            #region Profile
            CreateMap<ProfileEntity, ActivatedUserProfileResponseModel>();
            CreateMap<ProfileEntity, UserProfileModel>();
            CreateMap<UserProfileModel, ActivatedUserProfileResponseModel>();
            CreateMap<UserProfileModel, UserProfileResponseModel>();
            CreateMap<UpdateUserProfileRequestModel, UpdateUserProfileModel>();
            CreateMap<UserRatingModel, UserRatingResponseModel>();
            #endregion Profile

            #region Occupation
            CreateMap<OccupationEntity, OccupationModel>();
            CreateMap<OccupationModel, OccupationResponseModel>();
            #endregion Occupation
        }
    }
}
