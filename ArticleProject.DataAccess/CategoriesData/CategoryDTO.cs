using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArticleProject.DataAccess
{
    public class CategoryDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("category_name")]
        public string CategoryName { get; set; }
    }
}
