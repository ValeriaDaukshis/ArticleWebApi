﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class CreateCommentRequest
    {
        public string UserName { get; set; }
        public string CommentText { get; set; }
    }
}
