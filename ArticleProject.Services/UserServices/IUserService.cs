using ArticleProject.DataAccess;
using ArticleProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string id);
        Task<User> CreateUser(UpdateUserRequest user);
        Task<User> UpdateUser(string id, UpdateUserRequest user);
        Task RemoveUser(string id);
    }
}
