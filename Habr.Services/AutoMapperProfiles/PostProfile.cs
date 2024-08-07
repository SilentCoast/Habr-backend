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
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishedDate))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Where(p => p.ParentCommentId == null)));

            CreateMap<Post, DraftedPostDto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

            CreateMap<Post, PublishedPostDto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AuthorEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishedDate));
        }
    }
}
