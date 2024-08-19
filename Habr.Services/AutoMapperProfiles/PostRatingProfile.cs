using AutoMapper;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;

namespace Habr.Services.AutoMapperProfiles
{
    public class PostRatingProfile : Profile
    {
        public PostRatingProfile()
        {
            CreateMap<PostRating, PostRatingDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RatingStars, opt => opt.MapFrom(src => src.RatingStars))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RatedAt, opt => opt.MapFrom(src => src.RatedAt));
        }
    }
}
