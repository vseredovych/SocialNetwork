﻿using MVC.Models.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class PostItemViewModel
    {
        public string Id { get; set; }

        public string AuthorEmail { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurname { get; set; }

        public DateTime Timestamp { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public Comment[] Comments { get; set; }
    }
}
