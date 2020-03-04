using ArticleProject.DataAccess;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string request);
        Task<User> CreateUser(CreateUserRequest user);
        Task<User> UpdateUser(string id, CreateUserRequest user);
        Task RemoveUser(string id);
        Task<User> LogIn(VerifyUserRequest request);
    }
}
