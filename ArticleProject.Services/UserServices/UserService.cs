using ArticleProject.DataAccess;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticleProject.Services.ExceptionClasses;
using ArticleProject.Services.UserRepository;
using ArticleProject.Services.UserServices;
using ArticlesProject.JWT;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetUsers();
        }
        
        public async Task<UserToken> LogIn(VerifyUserRequest request, IJwtSigningEncodingKey signingEncodingKey)
        {
            return await _repository.LogIn(request, signingEncodingKey);
        }

        public async Task<ResponseUser> GetUser(string id)
        {
            return await _repository.GetUser(id);
        }

        public async Task<UserToken> CreateUser(CreateUserRequest createRequest, IJwtSigningEncodingKey signingEncodingKey)
        {
            return await _repository.CreateUser(createRequest, signingEncodingKey);
        }

        public async Task<UserToken> UpdateUser(string id, CreateUserRequest updateRequest, IJwtSigningEncodingKey signingEncodingKey)
        {
            return await _repository.UpdateUser(id, updateRequest, signingEncodingKey);
        }

        public async Task RemoveUser(string id)
        {
            await _repository.RemoveUser(id);
        }
    }
}
