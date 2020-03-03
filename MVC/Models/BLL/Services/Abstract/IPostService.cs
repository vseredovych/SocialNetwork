using MVC.Models.BLL.DTOs;
using System.Collections.Generic;

namespace MVC.Models.BLL.Services
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
    }
}
