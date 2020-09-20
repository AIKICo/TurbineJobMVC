using System.Collections.Generic;
using TurbineJobMVC.Models.Entities;

namespace TurbineJobMVC.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}