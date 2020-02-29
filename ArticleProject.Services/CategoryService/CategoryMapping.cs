using ArticleProject.DataAccess;
using ArticleProject.Models;
using ArticleProject.Models.CategoryModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Services
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<UpdateCategoryRequest, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
