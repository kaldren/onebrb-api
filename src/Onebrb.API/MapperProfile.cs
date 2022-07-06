﻿using Onebrb.API.Models;
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

            CreateMap<Profile, ActivatedProfileResponseModel>();
        }
    }
}
