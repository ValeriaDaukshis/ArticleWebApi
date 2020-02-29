using ArticleProject.Models;
using ArticleProject.Models.ArticleModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleService
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(string id);
        Task<Article> CreateArticle(UpdateArticleRequest request);
        Task<Article> UpdateArticle(string id, UpdateArticleRequest request);
        Task RemoveArticle(string id);
    }
}
