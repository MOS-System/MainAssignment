using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.DTOs.Responses.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public bool IsFavorite { get; set; }    // true if current user favorited it
    }
}
