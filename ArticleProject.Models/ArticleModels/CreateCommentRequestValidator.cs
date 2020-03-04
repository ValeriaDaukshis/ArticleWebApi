using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.CommentText).Length(4, 200);
        }
    }
}
