using ArticlesProject.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.DataAccess.ArticlesData
{
    public class ArticleContext : IArticleContext
    {
        private readonly IMongoDatabase _database;

        public ArticleContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ArticleDTO> Articles => _database.GetCollection<ArticleDTO>("articles");
    }
}
