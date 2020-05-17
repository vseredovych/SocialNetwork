using MVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ImageSource { get; set; }

        public List<Friend> Friends { get; set; }

        public int ShortestPathLength { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAuthorized { get; set; } = false;
    }
}
