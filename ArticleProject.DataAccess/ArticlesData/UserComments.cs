using ArticleProject.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.DataAccess.ArticlesData
{
    public class UserComments
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user")]
        public string UserId { get; set; }

        [BsonElement("comment_date")]
        public string CreatedDate { get; set; }

        [BsonElement("comment_text")]
        public string CommentText { get; set; }
    }
}
