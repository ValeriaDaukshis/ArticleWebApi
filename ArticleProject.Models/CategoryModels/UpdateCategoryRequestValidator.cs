using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.CategoryModels
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.CategoryName).Length(4, 60);
        }
    }
}
