using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // the product (for products page), make a dummy class since no need for real products
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string IconUrl { get; private set; }

        public Product(string name, string description, string iconUrl)
        {
            Name = name;
            Description = description;
            IconUrl = iconUrl;
        }

        private Product() { }
    }
}
