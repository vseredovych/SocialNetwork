using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class PostIndexViewModel
    {
        public IEnumerable<PostItemViewModel> PostItems { get; set; }
    }
}
