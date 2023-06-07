﻿using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? CategoryName { get; set; }
        public List<ProductImage> ? Images { get; set; }
        public decimal Price { get; set; }
        public string ? Description { get; set; }
    }
}
