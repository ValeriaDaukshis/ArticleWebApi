using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ArticleProject.DataAccess.ArticlesData
{
    public class ArticleDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("photo")]
        public string Photo { get; set; }

        [BsonElement("created_date")]
        public string CreatedDate { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("created_user")]
        public string UserId { get; set; }

        [BsonElement("category_name")]
        public string CategoryName { get; set; }

        [BsonElement("comments")]
        public UserComments[] Comments { get; set; }

    }
}
