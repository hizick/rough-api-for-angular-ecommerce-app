using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCommerceData.Models
{
    public partial class Products
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
