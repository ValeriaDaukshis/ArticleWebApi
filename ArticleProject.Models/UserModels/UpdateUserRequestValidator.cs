using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name).Length(4, 60);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 20);
        }
    }
}
