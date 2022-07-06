using Onebrb.Application.Comments.Models;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Comment, GetSingleCommentByIdModel>()
                .ForMember(
                    dest => dest.AuthorFirstName,
                    opt => opt.MapFrom(src => src.Author.FirstName)
                )
                .ForMember(
                    dest => dest.AuthorLastName,
                    opt => opt.MapFrom(src => src.Author.LastName)
                );
        }
    }
}
