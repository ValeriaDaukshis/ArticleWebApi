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
        public async Task<ResponseArticle> CreateArticle(UpdateArticleRequest createRequest)
        {
            return await _repository.CreateArticle(createRequest);
        }

        public async Task<ResponseArticle> GetArticle(string id)
        {
            return await _repository.GetArticle(id);
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticles()
        {
            return await _repository.GetArticles();
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticlesCategory(string categoryName)
        {
            return await _repository.GetArticlesCategory(categoryName);
        }

        public async Task RemoveArticle(string id)
        {
            await _repository.RemoveArticle(id);
        }

        public async Task<ResponseArticle> UpdateArticle(string id, UpdateArticleRequest updateRequest)
        {
            return await _repository.UpdateArticle(id, updateRequest);
        }

        public async Task<ResponseComment> CreateArticleComment(string id, CreateCommentRequest createRequest)
        {
            return await _repository.CreateArticleComment(id, createRequest);
        }

        public async Task<IEnumerable<ResponseComment>> GetArticleComments(string id)
        {
            return await _repository.GetArticleComments(id);
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticlesByUserId(string id)
        {
            return await _repository.GetArticlesByUserId(id);
        }
    }
}
