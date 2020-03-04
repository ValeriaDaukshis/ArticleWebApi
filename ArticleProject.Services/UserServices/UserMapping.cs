using ArticleProject.DataAccess;
using ArticleProject.Models;
using AutoMapper;

namespace ArticleProject.Services.UserServices
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserRequest, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
