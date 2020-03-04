using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.UserModels
{
    public class VerifyUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
