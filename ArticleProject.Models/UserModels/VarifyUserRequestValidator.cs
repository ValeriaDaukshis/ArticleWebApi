using FluentValidation;

namespace ArticleProject.Models.UserModels
{
    public class VarifyUserRequestValidator : AbstractValidator<VerifyUserRequest>
    {
        public VarifyUserRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 20);
        }
    }
}
