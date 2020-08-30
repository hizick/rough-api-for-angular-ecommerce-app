using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCommerceData.Models
{
    public partial class AccountDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
