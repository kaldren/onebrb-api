using Onebrb.Application.Comments.Models;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            #region Comments
            CreateMap<Comment, GetSingleCommentByCommentIdModel>()
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

            CreateMap<Comment, GetAllCommentsByUserIdModel>()
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
            #endregion Comments
        }
    }
}
