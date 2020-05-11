using ArticleProject.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class ResponseComment
    {
        public ResponseUser User { get; set; }

        public string CreatedDate { get; set; }

        public string CommentText { get; set; }
    }
}
