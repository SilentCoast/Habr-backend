using AutoMapper;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;

namespace Habr.Services.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostViewDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.AuthorEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Where(p => p.ParentCommentId == null)))
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.AverageRating))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.PostRatings));

            CreateMap<Post, DraftedPostDto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedAt));

            CreateMap<Post, DraftedPostViewDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt));

            CreateMap<Post, PublishedPostDto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AuthorEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt));

            CreateMap<Post, PublishedPostV2Dto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.AverageRating));
        }
    }
}
