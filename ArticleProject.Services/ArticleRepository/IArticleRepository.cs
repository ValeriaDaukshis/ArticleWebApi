using ArticleProject.Models.ArticleModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleRepository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ResponseArticle>> GetArticles();
        Task<ResponseArticle> GetArticle(string id);
        Task<IEnumerable<ResponseArticle>> GetArticlesByUserId(string id);
        Task<ResponseArticle> CreateArticle(UpdateArticleRequest request);
        Task<ResponseArticle> UpdateArticle(string id, UpdateArticleRequest request);
        Task RemoveArticle(string id);
        Task<IEnumerable<ResponseArticle>> GetArticlesCategory(string categoryName);
        Task<ResponseComment> CreateArticleComment(string id, CreateCommentRequest createRequest);
        Task<IEnumerable<ResponseComment>> GetArticleComments(string id);
    }
}
