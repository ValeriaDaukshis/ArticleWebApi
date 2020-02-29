using ArticleProject.DataAccess.ArticlesData;
using ArticleProject.Models.ArticleModels;
using AutoMapper;
using System;

namespace ArticleProject.Services.ArticleService
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<UpdateArticleRequest, ArticleDTO>()
                .ForMember(r => r.CreatedDate, opt => opt.MapFrom(p => DateTime.UtcNow));
            CreateMap<ArticleDTO, Article>();
        }
    }
}
