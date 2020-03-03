using MVC.Models.DAL.Entities;
using System;

namespace MVC.Models.BLL.DTOs
{
    public class PostDTO
    {
        public string _id { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurname { get; set; }

        public DateTime Timestamp { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public Comment[] Comments { get; set; }

    }
}