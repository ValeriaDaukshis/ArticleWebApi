using FluentValidation;

namespace ArticleProject.Models
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).Length(4, 60);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 20);
        }
    }
}
