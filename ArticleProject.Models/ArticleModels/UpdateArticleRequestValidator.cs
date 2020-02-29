using FluentValidation;

namespace ArticleProject.Models.ArticleModels
{
    public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            RuleFor(x => x.Description).Length(4, 2000);
            RuleFor(x => x.Title).Length(4, 100);
        }
    }
}
