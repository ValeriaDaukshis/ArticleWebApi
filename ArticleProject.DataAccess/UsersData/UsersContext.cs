using ArticlesProject.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ArticleProject.DataAccess.UsersData
{
    public class UsersContext : IUsersContext
    {
        private readonly IMongoDatabase _database;

        public UsersContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<UserDTO> Users => _database.GetCollection<UserDTO>("users");
    }
}
