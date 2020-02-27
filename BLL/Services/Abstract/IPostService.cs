using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
    }
}
