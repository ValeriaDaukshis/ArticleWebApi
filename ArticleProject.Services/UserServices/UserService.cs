using ArticleProject.DataAccess;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticleProject.Services.ExceptionClasses;
using ArticleProject.Services.UserRepository;
using ArticleProject.Services.UserServices;
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
        
        public async Task<User> LogIn(VerifyUserRequest request)
        {
            return await _repository.LogIn(request);
        }

        public async Task<User> GetUser(string id)
        {
            return await _repository.GetUser(id);
        }

        public async Task<User> CreateUser(CreateUserRequest createRequest)
        {
            return await _repository.CreateUser(createRequest);
        }

        public async Task<User> UpdateUser(string id, CreateUserRequest updateRequest)
        {
            return await _repository.UpdateUser(id, updateRequest);
        }

        public async Task RemoveUser(string id)
        {
            await _repository.RemoveUser(id);
        }
    }
}
