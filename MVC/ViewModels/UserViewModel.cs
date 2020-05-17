using MVC.Core.Entities;
using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string HashPassword { get; set; }

        public List<Friend> Friends { get; set; }

        public string ImageSource { get; set; }
    }
}
