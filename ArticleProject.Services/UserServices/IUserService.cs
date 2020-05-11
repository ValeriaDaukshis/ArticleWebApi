using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticlesProject.JWT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<ResponseUser> GetUser(string request);
        Task<UserToken> CreateUser(CreateUserRequest user, IJwtSigningEncodingKey signingEncodingKey);
        Task<UserToken> UpdateUser(string id, CreateUserRequest user, IJwtSigningEncodingKey signingEncodingKey);
        Task RemoveUser(string id);
        Task<UserToken> LogIn(VerifyUserRequest request, IJwtSigningEncodingKey signingEncodingKey);
    }
}
