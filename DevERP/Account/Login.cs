using System;
using System.Collections.Generic;
using System.Web;

namespace AbcoItAccountingApplication.Model.Login
{
    public class Login
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}