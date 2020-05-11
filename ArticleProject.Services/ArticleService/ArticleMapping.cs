using ArticleProject.DataAccess;
using ArticleProject.DataAccess.ArticlesData;
using ArticleProject.Models.ArticleModels;
using ArticleProject.Models.UserModels;
using AutoMapper;
using System;

namespace ArticleProject.Services.ArticleService
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<UpdateArticleRequest, ArticleDTO>()
                .ForMember(r => r.CreatedDate, opt => opt.MapFrom(p => DateTime.Now.ToShortDateString()));
            CreateMap<ArticleDTO, Article>();
            CreateMap<ArticleDTO, ResponseArticle>();
            CreateMap<UserDTO, ResponseUser>();
            CreateMap<UserComments, Comment>();
            CreateMap<UserComments, ResponseComment>();
            CreateMap<CreateCommentRequest, UserComments>()
                .ForMember(r => r.CreatedDate, opt => opt.MapFrom(p => DateTime.Now.ToShortDateString()));
        }
    }
}
