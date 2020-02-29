using ArticleProject.DataAccess;
using ArticleProject.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Services.UserServices
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UpdateUserRequest, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
