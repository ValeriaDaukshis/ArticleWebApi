using ArticleProject.Models.CategoryModels;

namespace ArticleProject.Models.ArticleModels
{
    public class UpdateArticleRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UpdateCategoryRequest Category { get; set; }
        public UpdateUserRequest User { get; set; }
    }
}
