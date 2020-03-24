using ArticleProject.Models.ArticleModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleRepository
{
    public interface  IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(string id);
        Task<Article> CreateArticle(UpdateArticleRequest request);
        Task<Article> UpdateArticle(string id, UpdateArticleRequest request);
        Task RemoveArticle(string id);
        Task<IEnumerable<Article>> GetArticlesCategory(string categoryName);
        Task<Comment> CreateArticleComment(string id, CreateCommentRequest createRequest);
        Task<IEnumerable<Comment>> GetArticleComments(string id);
    }
}
