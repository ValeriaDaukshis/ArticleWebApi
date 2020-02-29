using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.DataAccess
{
    public interface ICategoriesContext
    {
        IMongoCollection<CategoryDTO> Categories { get; }
    }
}
