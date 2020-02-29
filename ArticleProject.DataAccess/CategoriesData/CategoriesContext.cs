using ArticlesProject.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ArticleProject.DataAccess
{
    public class CategoriesContext : ICategoriesContext
    {
        private readonly IMongoDatabase _database;

        public CategoriesContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<CategoryDTO> Categories => _database.GetCollection<CategoryDTO>("categories");
    }
}
