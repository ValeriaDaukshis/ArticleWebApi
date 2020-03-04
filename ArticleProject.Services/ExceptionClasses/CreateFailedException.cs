using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Services.ExceptionClasses
{
    public class CreateFailedException : Exception
    {
        public CreateFailedException(string message)
        : base(message)
        {
        }
    }

    public class NotFoundItemException : Exception
    {
        public NotFoundItemException(string message)
        : base(message)
        {
        }
    }

    public class UpdateFailedException : Exception
    {
        public UpdateFailedException(string message)
        : base(message)
        {
        }
    }
}
