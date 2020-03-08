using System;

namespace MVC.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurname { get; set; }

        public string AuthorImageSource { get; set; }

        public DateTime Timestamp { get; set; }

        public string Text { get; set; }
    }
}
