using ArticleProject.DataAccess;
using ArticleProject.DataAccess.ArticlesData;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models.ArticleModels;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleContext _context;
        private readonly ICategoriesContext _categoryContext;
        private readonly IUsersContext _userContext;
        private readonly IMapper _mapper;

        public ArticleService(IArticleContext context, ICategoriesContext categoryContext, IUsersContext userContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
            _categoryContext = categoryContext;
        }
        public async Task<Article> CreateArticle(UpdateArticleRequest createRequest)
        {
            if (createRequest.User is null)
            {
                throw new ArgumentException("No user");
            }
            

            var dbArticles = await _context.Articles.Find(h => h.Title == createRequest.Title).ToListAsync();
            var dbCategory = await _categoryContext.Categories.Find(c => c.CategoryName == createRequest.Category.CategoryName).FirstOrDefaultAsync();
            //!!!!!!!!!!!!!!!!!!
            // look here if smth wrong
            var dbUser = await _userContext.Users.Find(c => c.Name == createRequest.User.Name && c.Email == createRequest.User.Email && c.Password == createRequest.User.Password).FirstOrDefaultAsync();

            if (dbArticles.Count > 0)
            {
                //throw new RequestedResourceHasConflictException("address");
            }

            if (dbUser is null)
            {
                throw new ArgumentException("No user");
            }

            if (dbCategory is null)
            {
                throw new ArgumentException("No category");
            }

            var dbArticle = _mapper.Map<UpdateArticleRequest, ArticleDTO>(createRequest);
            dbArticle.Category = _mapper.Map(createRequest.Category, dbCategory);
            dbArticle.User = _mapper.Map(createRequest.User, dbUser);

            await _context.Articles.InsertOneAsync(dbArticle);

            return _mapper.Map<Article>(dbArticle);
        }

        public async Task<Article> GetArticle(string id)
        {
            var dbArticles = await _context.Articles.Find(new BsonDocument("_id", new ObjectId(id))).ToListAsync();
            if (dbArticles.Count == 0)
            {
                //throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<ArticleDTO, Article>(dbArticles[0]);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            var dbArticles = await _context.Articles.Find(_ => true).ToListAsync();
            var articles = new List<Article>();
            foreach (var art in dbArticles)
            {
                articles.Add(_mapper.Map<Article>(art));
            }

            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticlesCategory(string categoryName)
        {
            var dbArticles = await _context.Articles.Find(c => c.Category.CategoryName == categoryName).ToListAsync();
            var articles = new List<Article>();
            foreach (var art in dbArticles)
            {
                articles.Add(_mapper.Map<Article>(art));
            }

            return articles;
        }

        public async Task RemoveArticle(string id)
        {
            var dbArticles = await _context.Articles.Find(p => p.Id == id).ToListAsync();
            if (dbArticles.Count == 0)
            {
                //throw new RequestedResourceNotFoundException();
            }

            var dbArticle = dbArticles[0];
            //if (dbCourier.IsDeleted == false)
            //{
            //   // throw new RequestedResourceHasConflictException();
            //}

            await _context.Articles.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<Article> UpdateArticle(string id, UpdateArticleRequest updateRequest)
        {
            if (updateRequest.User is null)
            {
                throw new ArgumentException("No user");
            }

            var dbArticles = await _context.Articles.Find(p => p.Title == updateRequest.Title && p.Id != id).ToListAsync();
            if (dbArticles.Count > 0)
            {
                //throw new RequestedResourceHasConflictException("address");
            }
            //!!!!!!!!!!!!!!!!!!
            // look here if smth wrong
            var dbCategory = await _categoryContext.Categories.Find(c => c.CategoryName == updateRequest.Category.CategoryName).FirstOrDefaultAsync();
            var dbUser = await _userContext.Users.Find(c => c.Name == updateRequest.User.Name && c.Email == updateRequest.User.Email && c.Password == updateRequest.User.Password).FirstOrDefaultAsync();

            dbArticles = _context.Articles.Find(p => p.Id == id).ToList();
            if (dbArticles.Count == 0)
            {
                // throw new RequestedResourceNotFoundException();
            }

            if (dbUser is null)
            {
                throw new ArgumentException("No user");
            }

            if (dbCategory is null)
            {
                throw new ArgumentException("No category");
            }

            var dbArticle = dbArticles[0];

            _mapper.Map(updateRequest, dbArticle);
            _mapper.Map(updateRequest.Category, dbCategory);
            _mapper.Map(updateRequest.User, dbUser);

            await _context.Articles.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), dbArticle);

            return _mapper.Map<Article>(dbArticle);
        }

        private string GetCategoryId(string name)
        {
            var dbCategory = _categoryContext.Categories.Find(p => p.CategoryName == name).FirstOrDefault();
            return dbCategory.Id;
        }
    }
}
