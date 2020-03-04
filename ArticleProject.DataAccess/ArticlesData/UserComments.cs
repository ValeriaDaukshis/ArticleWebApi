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

        [BsonElement("user_name")]
        public string UserName { get; set; }

        [BsonElement("comment_date")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("comment_text")]
        public string CommentText { get; set; }
    }
}
