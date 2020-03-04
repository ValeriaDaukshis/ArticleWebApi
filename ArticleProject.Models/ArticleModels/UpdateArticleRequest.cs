using ArticleProject.Models.CategoryModels;
using System;

namespace ArticleProject.Models.ArticleModels
{
    public class UpdateArticleRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UpdateCategoryRequest Category { get; set; }
        public CreateUserRequest User { get; set; }
        public CreateCommentRequest[] Comments => Array.Empty<CreateCommentRequest>();
    }
}
