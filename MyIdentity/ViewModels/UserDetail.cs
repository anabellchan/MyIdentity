using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIdentity.ViewModels
{
    public class UserDetail
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}