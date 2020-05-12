using ArticleProject.DataAccess;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using ArticleProject.Models.UserModels;
using ArticleProject.Services.ExceptionClasses;
using ArticlesProject.JWT;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArticleProject.Services.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUsersContext _context;
        private readonly IMapper _mapper;
        public UserRepository(IUsersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var dbUsers = await _context.Users.Find(_ => true).ToListAsync();
            var users = new List<User>();
            foreach (var user in dbUsers)
            {
                users.Add(_mapper.Map<User>(user));
            }

            return users;
        }

        public async Task<UserToken> LogIn(VerifyUserRequest request, IJwtSigningEncodingKey signingEncodingKey)
        {
            //request.Password = Crypto.ComputeSha256Hash(request.Password);
            var dbUsers = await _context.Users.Find(us => us.Email == request.Email && us.Password == request.Password).ToListAsync();
            if (dbUsers.Count == 0)
            {
                throw new CreateFailedException("Incorrect email or password");
            }
            var u = _mapper.Map<User>(dbUsers[0]);

            string token = CreateToken(u, signingEncodingKey);

            UserToken user = new UserToken
            {
                Id = u.Id,
                Token = token,
                Name = u.Name,
            };

            return user;
        }

        public async Task<ResponseUser> GetUser(string id)
        {
            var dbUsers = await _context.Users.Find(us => us.Id == id).ToListAsync();
            if (dbUsers.Count == 0)
            {
                throw new NotFoundItemException("No user found");
            }

            return _mapper.Map<UserDTO, ResponseUser>(dbUsers[0]);
        }

        public async Task<UserToken> CreateUser(CreateUserRequest createRequest, IJwtSigningEncodingKey signingEncodingKey)
        {
            //createRequest.Password = Crypto.ComputeSha256Hash(createRequest.Password);
            var dbUsers = await _context.Users.Find(us => us.Email == createRequest.Email && us.Password == createRequest.Password).ToListAsync();

            if (dbUsers.Count > 0)
            {
                throw new RequestedResourceHasConflictException("Create failed. Change email");
            }

            var dbUser = _mapper.Map<CreateUserRequest, UserDTO>(createRequest);
            await _context.Users.InsertOneAsync(dbUser);
            var u = _mapper.Map<User>(dbUser);

            string token = CreateToken(u, signingEncodingKey);


            UserToken user = new UserToken
            {
                Id = u.Id,
                Token = token,
                Name = u.Name,
            };

            return user;
        }

        public async Task<UserToken> UpdateUser(string id, CreateUserRequest updateRequest, IJwtSigningEncodingKey signingEncodingKey)
        {
            var dbUsers = await _context.Users.Find(p => p.Email == updateRequest.Email && p.Id != id).ToListAsync();
            if (dbUsers.Count > 0)
            {
                throw new RequestedResourceHasConflictException("Change password");
            }

            dbUsers = _context.Users.Find(p => p.Id == id).ToList();
            if (dbUsers.Count == 0)
            {
                throw new NotFoundItemException("No user found");
            }

            var dbUser = dbUsers[0];

            _mapper.Map(updateRequest, dbUser);

            await _context.Users.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(id)), dbUser);
            var u = _mapper.Map<User>(dbUser);

            string token = CreateToken(u, signingEncodingKey);


            UserToken user = new UserToken
            {
                Id = u.Id,
                Token = token,
                Name = u.Name,
            };

            return user;
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

        private string CreateToken(User createRequest, IJwtSigningEncodingKey signingEncodingKey)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, createRequest.Name),
                new Claim(ClaimTypes.Email, createRequest.Email),
            };

            var token = new JwtSecurityToken(
                issuer: "DemoApp",
                audience: "DemoAppClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
