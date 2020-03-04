using ArticleProject.Models;
using ArticleProject.Models.CategoryModels;
using ArticleProject.Services.CategoryRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category> CreateCategory(UpdateCategoryRequest createRequest)
        {
            return await _repository.CreateCategory(createRequest);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _repository.GetCategories();
        }


        public async Task<Category> GetCategory(string id)
        {
            return await _repository.GetCategory(id);
        }

        public async Task Removecategory(string id)
        {
            await _repository.Removecategory(id);
        }

        public async Task<Category> UpdateCategory(string id, UpdateCategoryRequest updateRequest)
        {
            return await _repository.UpdateCategory(id, updateRequest);
        }
    }
}
