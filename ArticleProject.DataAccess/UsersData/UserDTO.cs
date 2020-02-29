using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArticleProject.DataAccess
{
    public class UserDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        /* public string ImageId { get; set; } 

         public bool HasImage()
         {
             return !String.IsNullOrWhiteSpace(ImageId);
         }*/
    }
}
