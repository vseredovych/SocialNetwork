using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MVC.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("authorEmail")]
        public string AuthorEmail { get; set; }

        [BsonElement("authorName")]
        public string AuthorName { get; set; }

        [BsonElement("authorSurname")]
        public string AuthorSurname { get; set; }

        [BsonElement("authorImageSource")]
        public string AuthorImageSource { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("likes")]
        public List<Like> Likes { get; set; }
        
        [BsonElement("views")]
        public int Views { get; set; } = 0;

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

    }
}
