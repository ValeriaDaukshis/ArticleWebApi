using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.DataAccess.ArticlesData
{
    public class ArticleDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("created_date")]
        public string CreatedDate { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("created_user")]
        public UserDTO User { get; set; }

        [BsonElement("category")]
        public CategoryDTO Category { get; set; }

        [BsonElement("comments")]
        public UserComments[] Comments { get; set; }

    }
}
