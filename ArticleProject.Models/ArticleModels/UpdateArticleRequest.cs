using ArticleProject.Models.CategoryModels;
using System;
using System.Reflection.Metadata;

namespace ArticleProject.Models.ArticleModels
{
    public class UpdateArticleRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public CreateCommentRequest[] Comments => Array.Empty<CreateCommentRequest>();
    }
}
