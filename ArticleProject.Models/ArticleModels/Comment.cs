using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class Comment
    {
        public string UserName { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string CommentText { get; set; }
    }
}
