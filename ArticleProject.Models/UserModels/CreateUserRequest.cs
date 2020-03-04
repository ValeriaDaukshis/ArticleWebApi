using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
