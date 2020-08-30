using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheCommerceData.Models;

namespace TheCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriesController: Controller
    {
        private TheCommerceContext context;
        public CategoriesController(TheCommerceContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            var category = context.Category.ToList();
            if (category == null)
                return null;
            return category;
        }
    }
}
