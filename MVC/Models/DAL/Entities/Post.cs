using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MVC.Models.DAL.Entities
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("authorEmail")]
        public string AuthorEmail { get; set; }

        [BsonElement("authorName")]
        public string AuthorName { get; set; }

        [BsonElement("authorSurname")]
        public string AuthorSurname { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("comments")]
        public Comment[] Comments { get; set; }

    }
}
