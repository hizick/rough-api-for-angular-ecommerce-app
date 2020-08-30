using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheCommerceAPI.DomainModel;
using TheCommerceAPI.Misc;
using TheCommerceData.Models;

namespace TheCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShoppingCartController: Controller
    {
        private TheCommerceContext context;
        public ShoppingCartController(TheCommerceContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public ActionResult<StatusMessage> AddToCart(ProductCartModel productCart)
        {
            StatusMessage result = new StatusMessage(); 
            ShoppingCart item = new ShoppingCart();
            try
            {
                if (string.IsNullOrEmpty(productCart.ShoppingCartId))
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Misc", "ProductId.txt");
                    string id = System.IO.File.ReadAllText(path);
                    productCart.ShoppingCartId = getCartId(id);
                }
                //else
                //{
                //    item.ShoppingCartId = productCart.ShoppingCartId;

                //}

                var itemL = getItem(productCart.ShoppingCartId, productCart.ProductId);
                if (itemL == null)
                {
                    item.DateCreated = DateTime.Now;
                    item.ProductId = productCart.ProductId;
                    item.ShoppingCartId = productCart.ShoppingCartId;
                    item.ItemQuantity = 1;
                    context.Add(item);
                }
                else
                {
                    item.DateCreated = DateTime.Now;
                    item.ProductId = productCart.ProductId;
                    item.ShoppingCartId = productCart.ShoppingCartId;
                    item.ItemQuantity = itemL.ItemQuantity + 1;
                    context.Update(item);
                }
                context.SaveChanges();
                result.Message = item.ShoppingCartId;
                result.Status = true;
            }
            catch (Exception ex)
            {
                result.Message = "add to cart Failed";
            }

            return result;
        }

        [HttpGet("{cartId}")]
        public ActionResult<List<ShoppingCartItem>> Get(string cartId)
        {
            List <ShoppingCartItem> result = new List<ShoppingCartItem>();
            var shoppingCart = context.ShoppingCart.Where(x => x.ShoppingCartId == cartId).ToList();
            if (shoppingCart == null)
                return null;
            foreach (ShoppingCart item in shoppingCart)
            {
                //ShoppingCartItem shoppingcartItem = new ShoppingCartItem();
                result.Add(new ShoppingCartItem
                {
                    ShoppingCartId = item.ShoppingCartId,
                    ProductId = item.ProductId,
                    ItemQuantity = item.ItemQuantity,
                    DateCreated = item.DateCreated,
                    ProductList = getProductList(item.ProductId)
                });
            }    
            return result;
        }

        public Products getProductList(string cartId)
        {
            Products ProductList = new Products();
            //var ShoppingList = context.ShoppingCart.Where(x => x.ShoppingCartId == cartId).ToList();
            //foreach (var prdct in ShoppingList)
            //{
                var product = context.Products.FirstOrDefault(x => x.ProductId == cartId);
                //ProductList.Add(product);
            //}
            return product;
        }
        private ShoppingCart getItem(string cartId, string productId)
        {
            ShoppingCart item = context.ShoppingCart.FirstOrDefault(x => x.ShoppingCartId == cartId && x.ProductId == productId);
            return item;
        }

        public string getCartId(string n)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Misc", "ProductId.txt");
            string productId = "SHPC";
            int i = Convert.ToInt32(n);
            i++;
            string vproductId = Convert.ToString(i);
            productId = productId + vproductId;
            System.IO.File.WriteAllText(path, vproductId);
            return productId;
        }
    }
}
