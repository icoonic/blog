﻿using Blog.Entitiy.ViewModels_DTOs.Categories;

namespace Blog.Entitiy.ViewModels_DTOs.Articles
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public CategoryViewModel Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set;}
        public bool IsDeleted { get; set; }

    }
}