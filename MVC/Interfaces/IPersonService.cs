using MVC.Core.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IPersonService
    {
        void CreatePerson(User user);
        void DeletePerson(string email);
        void CreateRelationship(string email1, string email2);
        void DeleteRelationship(string email1, string email2);
        int GetShortestPathLength(string email1, string email2);
    }
}
