using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class PostIndexViewModel
    {
        public IEnumerable<PostItemViewModel> PostItems { get; set; }
    }
}
