using ArticleProject.DataAccess;
using ArticleProject.DataAccess.ArticlesData;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models.ArticleModels;
using ArticleProject.Models.UserModels;
using ArticleProject.Services.ExceptionClasses;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services.ArticleRepository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IArticleContext _context;
        private readonly ICategoriesContext _categoryContext;
        private readonly IUsersContext _userContext;
        private readonly IMapper _mapper;

        public ArticleRepository(IArticleContext context, ICategoriesContext categoryContext, IUsersContext userContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
            _categoryContext = categoryContext;
        }
        public async Task<ResponseArticle> CreateArticle(UpdateArticleRequest createRequest)
        {
            if (createRequest.UserId is null)
            {
                throw new ArgumentException("No user");
            }


            var dbArticles = await _context.Articles.Find(h => h.Title == createRequest.Title).ToListAsync();
            var dbCategory = await _categoryContext.Categories.Find(c => c.CategoryName == createRequest.CategoryName).FirstOrDefaultAsync();
            //!!!!!!!!!!!!!!!!!!
            // look here if smth wrong
            var dbUser = await _userContext.Users.Find(new BsonDocument("_id", new ObjectId(createRequest.UserId))).ToListAsync();

            if (dbArticles.Count > 0)
            {
                throw new RequestedResourceHasConflictException("address");
            }

            if (dbUser is null)
            {
                throw new NotFoundItemException("User not found");
            }

            if (dbCategory is null)
            {
                throw new NotFoundItemException("Category not found");
            }

            var dbArticle = _mapper.Map<UpdateArticleRequest, ArticleDTO>(createRequest);

            await _context.Articles.InsertOneAsync(dbArticle);

            var article = await GetFullUserInfo(dbArticle);
            return article;
        }

        public async Task<ResponseArticle> GetArticle(string id)
        {
            var dbArticles = await _context.Articles.Find(new BsonDocument("_id", new ObjectId(id))).ToListAsync();
            if (dbArticles.Count == 0)
            {
                throw new NotFoundItemException("Article not found");
            }

            var article = await GetFullUserInfo(dbArticles[0]);
            return article;
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticlesByUserId(string id)
        {
            var dbArticles = await _context.Articles.Find(new BsonDocument("created_user", new ObjectId(id))).ToListAsync();
            if (dbArticles.Count == 0)
            {
                throw new NotFoundItemException("Article not found");
            }
            var articles = new List<ResponseArticle>();
            foreach (var art in dbArticles)
            {
                var article = await GetFullUserInfo(art);
                articles.Add(article);
            }

            return articles;
        }

        public async Task<IEnumerable<ResponseComment>> GetArticleComments(string id)
        {
            var article = await GetArticle(id);

            return article.Comments;
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticles()
        {
            var dbArticles = await _context.Articles.Find(_ => true).ToListAsync();
            var articles = new List<ResponseArticle>();
            foreach (var art in dbArticles)
            {
                var article = await GetFullUserInfo(art);
                articles.Add(article);
            }

            return articles;
        }

        public async Task<IEnumerable<ResponseArticle>> GetArticlesCategory(string categoryName)
        {
            var dbArticles = await _context.Articles.Find(c => c.CategoryName == categoryName).ToListAsync();
            var articles = new List<ResponseArticle>();
            foreach (var art in dbArticles)
            {
                var article = await GetFullUserInfo(art);
                articles.Add(article);
            }

            return articles;
        }

        public async Task RemoveArticle(string id)
        {
            var dbArticles = await _context.Articles.Find(p => p.Id == id).ToListAsync();
            if (dbArticles.Count == 0)
            {
                throw new NotFoundItemException("Article not found");
            }

            var dbArticle = dbArticles[0];

            await _context.Articles.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<ResponseArticle> UpdateArticle(string id, UpdateArticleRequest updateRequest)
        {
            if (updateRequest.UserId is null)
            {
                throw new ArgumentException("No user");
            }

            var dbArticles = await _context.Articles.Find(p => p.Title == updateRequest.Title && p.Id != id).ToListAsync();
            if (dbArticles.Count > 0)
            {
                throw new RequestedResourceHasConflictException("address");
            }
            //!!!!!!!!!!!!!!!!!!
            // look here if smth wrong
            var dbCategory = await _categoryContext.Categories.Find(c => c.CategoryName == updateRequest.CategoryName).FirstOrDefaultAsync();
            var dbUser = await _userContext.Users.Find(new BsonDocument("_id", new ObjectId(updateRequest.UserId))).ToListAsync();

            dbArticles = _context.Articles.Find(p => p.Id == id).ToList();

            if (dbArticles.Count == 0)
            {
                throw new NotFoundItemException("Article not found");
            }

            if (dbUser is null)
            {
                throw new NotFoundItemException("User not found");
            }

            if (dbCategory is null)
            {
                throw new NotFoundItemException("Category not found");
            }

            var dbArticle = dbArticles[0];

            _mapper.Map(updateRequest, dbArticle);

            await _context.Articles.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), dbArticle);

            var article = await GetFullUserInfo(dbArticle);
            return article;
        }

        public async Task<ResponseComment> CreateArticleComment(string id, CreateCommentRequest createRequest)
        {
            if (createRequest.UserId is null)
            {
                throw new NotFoundItemException("User not found");
            }

            var dbComment = _mapper.Map<CreateCommentRequest, UserComments>(createRequest);

            var filter = Builders<ArticleDTO>
             .Filter.Eq(e => e.Id, id);

            var update = Builders<ArticleDTO>.Update
                    .Push<UserComments>(e => e.Comments, dbComment);

            await _context.Articles.FindOneAndUpdateAsync(filter, update);

            var a = await GetFullCommentInfo(dbComment);
            return a;
        }

        private async Task<ResponseArticle> GetFullUserInfo(ArticleDTO art)
        {
            var article = _mapper.Map<ResponseArticle>(art);
            var user = await _userContext.Users.Find(c => c.Id == art.UserId).FirstOrDefaultAsync();
            article.User = _mapper.Map<ResponseUser>(user);
            List<ResponseComment> responseComments = new List<ResponseComment>();

            foreach (var comment in art.Comments)
            {
                var com = await GetFullCommentInfo(comment);
                responseComments.Add(com);
            }
            article.Comments = responseComments.ToArray();

            return article;
        }

        private async Task<ResponseComment> GetFullCommentInfo(UserComments comment)
        {
            var com = _mapper.Map<ResponseComment>(comment);
            var commentUser = await _userContext.Users.Find(c => c.Id == comment.UserId).FirstOrDefaultAsync();
            com.User = _mapper.Map<ResponseUser>(commentUser);
            return com;
        }

        private string GetCategoryId(string name)
        {
            var dbCategory = _categoryContext.Categories.Find(p => p.CategoryName == name).FirstOrDefault();
            return dbCategory.Id;
        }
    }
}
