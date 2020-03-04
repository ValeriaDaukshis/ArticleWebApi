using ArticleProject.Models;
using ArticleProject.Models.CategoryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(string id);
        Task<Category> CreateCategory(UpdateCategoryRequest request);
        Task<Category> UpdateCategory(string id, UpdateCategoryRequest request);
        Task Removecategory(string id);
    }
}
