using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Services.ExceptionClasses
{
    public class LogInFailedException : Exception
    {
        public LogInFailedException()
        : base("Log in failed!")
        {
        }
    }

    public class SingInFailedException : Exception
    {
        public SingInFailedException()
        : base("Sing in failed!")
        {
        }
    }
}
