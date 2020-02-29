using ArticleProject.Models.CategoryModels;

namespace ArticleProject.Models.ArticleModels
{
    public class Article
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }
    }
}
