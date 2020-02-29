using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.DataAccess.ArticlesData
{
    public interface IArticleContext
    {
        IMongoCollection<ArticleDTO> Articles { get; }
    }
}
