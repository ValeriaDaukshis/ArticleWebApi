using ArticleProject.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleProject.Models.ArticleModels
{
    public class ResponseArticle
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string CategoryName { get; set; }

        public ResponseUser User { get; set; }

        public ResponseComment[] Comments { get; set; }
    }
}
