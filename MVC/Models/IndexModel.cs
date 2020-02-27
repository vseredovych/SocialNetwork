using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class IndexModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(UnitOfWork uniteOfWork)
        {
            _unitOfWork = uniteOfWork;
        }
    }
}
