﻿using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entitiy.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {

        }
        public Category(string name)
        {
            Name = name;
        }
         
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }


    }
}
