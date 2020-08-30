using System;
using System.Collections.Generic;

namespace TheCommerceData.Models
{
    public partial class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        //public string ProductName { get; set; }
        //public string CategoryId { get; set; }
        //public double? Price { get; set; }
        //public string ImageUrl { get; set; }
        public int ItemQuantity { get; set; }
        public DateTime? DateCreated { get; set; }
        //public virtual List<Products> Products { get; set; }
    }
}
