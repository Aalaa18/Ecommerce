﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        
            public Category()
            {
                Products = new List<Product>();
            }
            public int Id { get; set; }
            [MaxLength(100)]
            public string Name { get; set; }
            public ICollection<Product> Products { get; set; }

        }
    
}
