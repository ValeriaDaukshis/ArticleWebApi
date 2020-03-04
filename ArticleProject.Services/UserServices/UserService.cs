using ArticleProject.DataAccess;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticleProject.Services.ExceptionClasses;
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
        private readonly IUsersContext _context;
        private readonly IMapper _mapper;
        public UserService(IUsersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var dbUsers = await _context.Users.Find(_ => true).ToListAsync();
            var users = new List<User>();
            foreach(var user in dbUsers)
            {
                users.Add(_mapper.Map<User>(user));
            }

            return users;
        }
        
        public async Task<User> LogIn(VerifyUserRequest request)
        {
            //request.Password = Crypto.ComputeSha256Hash(request.Password);
            var dbUsers = await _context.Users.Find(us=> us.Email == request.Email && us.Password == request.Password).ToListAsync();
            if (dbUsers.Count == 0)
            {
                throw new CreateFailedException();
            }

            return _mapper.Map<UserDTO, User>(dbUsers[0]);
        }

        public async Task<User> GetUser(string id)
        {
            var dbUsers = await _context.Users.Find(us => us.Id == id).ToListAsync();
            if (dbUsers.Count == 0)
            {
                throw new NotFoundItemException("No user found");
            }

            return _mapper.Map<UserDTO, User>(dbUsers[0]);
        }

        public async Task<User> CreateUser(CreateUserRequest createRequest)
        {
            //createRequest.Password = Crypto.ComputeSha256Hash(createRequest.Password);
            var dbUsers = await _context.Users.Find(us => us.Email == createRequest.Email && us.Password == createRequest.Password).ToListAsync();

            if (dbUsers.Count > 0)
            {
                throw new UpdateFailedException();
            }

            var dbUser = _mapper.Map<CreateUserRequest, UserDTO>(createRequest);
            await _context.Users.InsertOneAsync(dbUser);

            return _mapper.Map<User>(dbUser);
        }

        public async Task<User> UpdateUser(string id, CreateUserRequest updateRequest)
        {
            var dbUsers = await _context.Users.Find(p => p.Email == updateRequest.Email && p.Id != id).ToListAsync();
            if (dbUsers.Count > 0)
            {
                throw new UpdateFailedException("Change password");
            }

            dbUsers = _context.Users.Find(p => p.Id == id).ToList();
            if (dbUsers.Count == 0)
            {
                throw new NotFoundItemException("No user found");
            }

            var dbUser = dbUsers[0];

            _mapper.Map(updateRequest, dbUser);

            await _context.Users.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), dbUser);

            return _mapper.Map<User>(dbUser);
        }

        public async Task RemoveUser(string id)
        {
            var dbUsers = await _context.Users.Find(p => p.Id == id).ToListAsync();
            if (dbUsers.Count == 0)
            {
                throw new NotFoundItemException("User not found");
            }

            var dbUser = dbUsers[0];

            await _context.Users.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}
