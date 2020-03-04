using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class PostIndexViewModel
    {
        public IEnumerable<PostViewModel> PostItems { get; set; }
    }
}
