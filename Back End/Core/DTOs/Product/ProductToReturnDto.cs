﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Product
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);

        public int Condition { get; set; }
        public int stockQuantity { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? ProductDetailone { get; set; }
        public string? ProductDetailtwo { get; set; }
        public string? ProductDetailthree { get; set; }
        public string? ProductDetailfour { get; set; }
        public string? ProductDetailfive { get; set; }
        public string? ProductDetailsix { get; set; }
        public string? ProductDetailsiven { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public Dictionary<string, string> Warranties { get; set; } // part , descript
        public ICollection<string> Images { get; set; } 
        public decimal AvgRating { get; set; } // from review
        public decimal AvgRatingRounded => Math.Round(AvgRating, 1);
    }
}
