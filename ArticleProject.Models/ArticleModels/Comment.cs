﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class Comment
    {
        public string UserId { get; set; }
        
        public string CreatedDate { get; set; }
        
        public string CommentText { get; set; }
    }
}
