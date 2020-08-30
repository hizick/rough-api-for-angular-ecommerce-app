using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TheCommerceAPI.Misc;
using TheCommerceData.Models;

namespace TheCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : Controller
    {
        private TheCommerceContext context;
        public AccountController(TheCommerceContext _context)
        {
            context = _context;
        }
        // GET api/values/5
        [HttpGet]
        public ActionResult<List<AccountDetails>> Get()
        {
            var login = context.AccountDetails.ToList();
            if (login == null)
                return null;
            return login;
        }

        [HttpPost]
        public ActionResult<StatusMessage> Login([FromBody] AccountDetails accountDetails)
        {
            StatusMessage result = new StatusMessage();

            var login = context.AccountDetails
                                .FirstOrDefault(x => x.Username == accountDetails.Username 
                                && x.Password == accountDetails.Password);
            if (login != null)
                result.Status = true;
                result.Message = login == null ? "Invalid user" : login.Username;
                result.isAdmin = login == null ? false : login.IsAdmin;
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<AccountDetails> Get(int id)
        {
            var login = context.AccountDetails.FirstOrDefault(x => x.UserId == id);
            if (login == null)
                return null;
            return login;
        }

        [HttpPost("{accountDetails}")]
        public ActionResult<StatusMessage> Register(string accountDetails)
        {
            StatusMessage result = new StatusMessage();
            try
            {
                var RegisterInput = new AccountDetails();

                JsonConvert.PopulateObject(accountDetails, RegisterInput);
                if (!TryValidateModel(RegisterInput))
                    return Json(null);

                context.AccountDetails.Add(RegisterInput);
                context.SaveChanges();
                result.Status = true;
                result.Message = RegisterInput.Username;
            }
            catch (Exception ex)
            {
                result.Message = "Registration Error";
            }
            return result;
        }
    }
}
