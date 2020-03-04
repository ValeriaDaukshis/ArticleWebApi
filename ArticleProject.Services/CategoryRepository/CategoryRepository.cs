using ArticleProject.DataAccess;
using ArticleProject.Models;
using ArticleProject.Models.CategoryModels;
using ArticleProject.Services.ExceptionClasses;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoriesContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(ICategoriesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategory(UpdateCategoryRequest createRequest)
        {
            var dbCategories = await _context.Categories.Find(h => h.CategoryName == createRequest.CategoryName).ToListAsync();
            if (dbCategories.Count > 0)
            {
                throw new CreateFailedException("Create category failed. Change category name");
            }

            var dbCategory = _mapper.Map<UpdateCategoryRequest, CategoryDTO>(createRequest);
            await _context.Categories.InsertOneAsync(dbCategory);

            return _mapper.Map<Category>(dbCategory);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var dbCategories = await _context.Categories.Find(_ => true).ToListAsync();
            var categories = new List<Category>();
            foreach (var c in dbCategories)
            {
                categories.Add(_mapper.Map<Category>(c));
            }

            return categories;
        }


        public async Task<Category> GetCategory(string id)
        {
            var dbCategory = await _context.Categories.Find(new BsonDocument("_id", new ObjectId(id))).ToListAsync();
            if (dbCategory.Count == 0)
            {
                throw new NotFoundItemException("Category not found");
            }

            return _mapper.Map<CategoryDTO, Category>(dbCategory[0]);
        }

        public async Task Removecategory(string id)
        {
            var dbcategories = await _context.Categories.Find(p => p.Id == id).ToListAsync();
            if (dbcategories.Count == 0)
            {
                throw new NotFoundItemException("Category not found");
            }

            var dbcategory = dbcategories[0];

            await _context.Categories.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<Category> UpdateCategory(string id, UpdateCategoryRequest updateRequest)
        {
            var dbCategories = await _context.Categories.Find(p => p.CategoryName == updateRequest.CategoryName && p.Id != id).ToListAsync();
            if (dbCategories.Count > 0)
            {
                throw new RequestedResourceHasConflictException("address");
            }

            dbCategories = _context.Categories.Find(p => p.Id == id).ToList();
            if (dbCategories.Count == 0)
            {
                throw new NotFoundItemException("Category not found");
            }

            var dbCategory = dbCategories[0];

            _mapper.Map(updateRequest, dbCategory);

            await _context.Categories.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), dbCategory);

            return _mapper.Map<Category>(dbCategory);
        }
    }
}
