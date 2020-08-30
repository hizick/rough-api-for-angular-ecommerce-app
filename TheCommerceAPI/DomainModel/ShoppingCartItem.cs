using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCommerceData.Models;

namespace TheCommerceAPI.DomainModel
{
    public class ShoppingCartItem
    {
        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        public int ItemQuantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public Products ProductList { get; set; }
    }
}
