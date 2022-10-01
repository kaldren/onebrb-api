using Onebrb.API.Models.Requests;
using Onebrb.API.Models.Requests.Comment;
using Onebrb.API.Models.Responses;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Products.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProductAggregate;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.API
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            #region Comment
            CreateMap<Comment, CommentModel>()
                .ForMember(
                    dest => dest.AuthorFirstName,
                    opt => opt.MapFrom(src => src.Author.FirstName)
                )
                .ForMember(
                    dest => dest.AuthorLastName,
                    opt => opt.MapFrom(src => src.Author.LastName)
                );

            CreateMap<CommentModel, CommentResponseModel>();

            CreateMap<PostCommentRequestModel, PostCommentModel>();
            CreateMap<PatchCommentRequestModel, PatchCommentModel>();
            #endregion Comment

            #region Profile
            CreateMap<Profile, ActivatedUserProfileResponseModel>();
            CreateMap<Profile, UserProfileModel>();
            CreateMap<UserProfileModel, ActivatedUserProfileResponseModel>();
            CreateMap<UserProfileModel, UserProfileResponseModel>();
            CreateMap<UpdateUserProfileRequestModel, UpdateUserProfileModel>();
            #endregion Profile

            #region Product
            CreateMap<Product, ProductModel>();
            CreateMap<ProductImage, ProductImageModel>();
            CreateMap<ProductModel, ProductResponseModel>();
            CreateMap<ProductImageModel, ProductImageResponseModel>();
            #endregion Product
        }
    }
}
