using MongoDB.Driver;

namespace ArticleProject.DataAccess.UsersData
{
    public interface IUsersContext
    {
        IMongoCollection<UserDTO> Users { get; }
    }
}
