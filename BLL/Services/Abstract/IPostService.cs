using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Services
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
    }
}
