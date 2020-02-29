using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
