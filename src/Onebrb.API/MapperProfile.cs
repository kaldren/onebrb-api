using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Requests.Comment;
using Onebrb.API.Models.Responses.Comment;
using Onebrb.API.Models.Responses.Occupation;
using Onebrb.API.Models.Responses.Profile;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Occupations.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.API
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            #region Comment
            CreateMap<IQueryable<CommentEntity>, IQueryable<CommentModel>>();

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
                )
                .ForMember(
                    dest => dest.DateCreated,
                    opt => opt.MapFrom(src => src.DateCreated)
                );

            CreateMap<CommentModel, CommentResponseModel>()
                .ForMember(
                    dest => dest.DateCreated,
                    opt => opt.MapFrom(src => src.DateCreated)
                );

            CreateMap<PostCommentRequestModel, PostCommentModel>();
            CreateMap<PatchCommentRequestModel, PatchCommentModel>();
            #endregion Comment

            #region Profile
            CreateMap<UserEntity, ActivatedUserProfileResponseModel>();
            CreateMap<UserEntity, UserProfileModel>();
            CreateMap<UserProfileModel, ActivatedUserProfileResponseModel>();
            CreateMap<UserProfileModel, UserProfileResponseModel>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.HasPhoto ? $"https://onebrbstorage.blob.core.windows.net/{src.Id}/{src.Id}" : default));
            CreateMap<UpdateUserProfileRequestModel, UpdateUserProfileModel>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? null : s);

            CreateMap<UserRatingModel, UserRatingResponseModel>();
            #endregion Profile

            #region Occupation
            CreateMap<OccupationEntity, OccupationModel>();
            CreateMap<OccupationModel, OccupationResponseModel>();
            #endregion Occupation
        }
    }
}
