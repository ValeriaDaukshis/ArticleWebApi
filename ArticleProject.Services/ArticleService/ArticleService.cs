using ArticleProject.Models.ArticleModels;
using ArticleProject.Services.ArticleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }
        public async Task<Article> CreateArticle(UpdateArticleRequest createRequest)
        {
            return await _repository.CreateArticle(createRequest);
        }

        public async Task<Article> GetArticle(string id)
        {
            return await _repository.GetArticle(id);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _repository.GetArticles();
        }

        public async Task<IEnumerable<Article>> GetArticlesCategory(string categoryName)
        {
            return await _repository.GetArticlesCategory(categoryName);
        }

        public async Task RemoveArticle(string id)
        {
            await _repository.RemoveArticle(id);
        }

        public async Task<Article> UpdateArticle(string id, UpdateArticleRequest updateRequest)
        {
            return await _repository.UpdateArticle(id, updateRequest);
        }

        public async Task CreateArticleComment(string id, CreateCommentRequest createRequest)
        {
            await _repository.CreateArticleComment(id, createRequest);
        }
    }
}
