using ArticleProject.DataAccess;
using ArticleProject.DataAccess.UsersData;
using ArticleProject.Models;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleProject.Services
{
    public class UserService : IUserService
    {
        IGridFSBucket gridFS;
        IUsersContext _context;
        IMapper _mapper;
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
        
        public async Task<User> GetUser(string id)
        {
            var dbUsers = await _context.Users.Find(new BsonDocument("_id", new ObjectId(id))).ToListAsync();
            if (dbUsers.Count == 0)
            {
                //throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<UserDTO, User>(dbUsers[0]);
        }

        public async Task<User> CreateUser(UpdateUserRequest createRequest)
        {
            var dbUsers = await _context.Users.Find(h => h.Name == createRequest.Name).ToListAsync();
            if (dbUsers.Count > 0)
            {
                //throw new RequestedResourceHasConflictException("address");
            }

            var dbUser = _mapper.Map<UpdateUserRequest, UserDTO>(createRequest);
            await _context.Users.InsertOneAsync(dbUser);

            return _mapper.Map<User>(dbUser);
        }

        public async Task<User> UpdateUser(string id, UpdateUserRequest updateRequest)
        {
            var dbUsers = await _context.Users.Find(p => p.Name == updateRequest.Name && p.Id != id).ToListAsync();
            if (dbUsers.Count > 0)
            {
                //throw new RequestedResourceHasConflictException("address");
            }

            dbUsers = _context.Users.Find(p => p.Id == id).ToList();
            if (dbUsers.Count == 0)
            {
               // throw new RequestedResourceNotFoundException();
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
                //throw new RequestedResourceNotFoundException();
            }

            var dbUser = dbUsers[0];
            //if (dbCourier.IsDeleted == false)
            //{
            //   // throw new RequestedResourceHasConflictException();
            //}

            await _context.Users.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }


        //public async Task<byte[]> GetImage(string id)
        //{
        //    return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        //}

        //public async Task StoreImage(string id, Stream imageStream, string imageName)
        //{
        //    Product p = await GetProduct(id);
        //    if (p.HasImage())
        //    {

        //        await gridFS.DeleteAsync(new ObjectId(p.ImageId));
        //    }

        //    ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);

        //    p.ImageId = imageId.ToString();
        //    var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(p.Id));
        //    var update = Builders<Product>.Update.Set("ImageId", p.ImageId);
        //    await Products.UpdateOneAsync(filter, update);
        //}
    }
}
