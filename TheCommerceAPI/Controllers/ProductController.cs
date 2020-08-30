using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheCommerceAPI.Misc;
using TheCommerceData.Models;

namespace TheCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController: Controller
    {
        private TheCommerceContext context;

        public ProductController(TheCommerceContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult<List<Products>> Get()
        {
            var product = context.Products.ToList();
            if (product == null)
                return null;
            return product;
        }

        [HttpGet("{productId}")]
        public ActionResult<Products> Get(string productId)
        {
            var product = context.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
                return null;
            return product;
        }

        [HttpPut("{productId}")]
        public ActionResult<StatusMessage> UpdateProduct(string productId, [FromBody] Products products)
        {
            StatusMessage result = new StatusMessage();
            var product = context.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                try
                {
                    product.ProductId = product.ProductId;
                    product.ProductName = products.ProductName == null ? product.ProductName : products.ProductName;
                    product.Price = products.Price == null ? product.Price : products.Price;
                    product.CategoryId = products.CategoryId == null ? product.CategoryId : products.CategoryId;
                    product.ImageUrl = products.ImageUrl == null ? product.ImageUrl : products.ImageUrl;
                    context.Update(product);
                    context.SaveChanges();
                    result.Status = true;
                    result.Message = "Success";
                }
                catch (Exception ex)
                {
                    result.Message = "Product update failed.";
                }

            }
            return result;
        }

        [HttpPost]
        public ActionResult<StatusMessage> CreateProduct(Products products)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Misc", "ProductId.txt");
            string id = System.IO.File.ReadAllText(path);
            products.ProductId = getProductId(id);
            
            StatusMessage result = new StatusMessage();
            try
            {
                context.Products.Add(products);
                context.SaveChanges();

                result.Status = true;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = "Product couldn't be saved.";
            }
            return result;
        }

        [HttpDelete("{productId}")]
        public ActionResult<StatusMessage> DeleteProduct(string productId)
        {
            StatusMessage result = new StatusMessage();
            var product = context.Products.FirstOrDefault(x => x.ProductId == productId);
            try
            {
                context.Remove(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = "Error";
            }
            return result; 
        }

        public string getProductId(string n)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Misc", "ProductId.txt");
            string productId = "PRDT";
            int i = Convert.ToInt32(n);
            i++;
            string vproductId = Convert.ToString(i);
            productId = productId + vproductId;
            System.IO.File.WriteAllText(path, vproductId);
            return productId;
        }

    }
}
