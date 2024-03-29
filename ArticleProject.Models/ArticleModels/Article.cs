﻿using ArticleProject.Models.CategoryModels;
using System.Reflection.Metadata;

namespace ArticleProject.Models.ArticleModels
{
    public class Article
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string CategoryName { get; set; }

        public string UserId { get; set; }

        public Comment[] Comments { get; set; }
    }
}
