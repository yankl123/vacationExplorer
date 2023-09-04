using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dache.DP
{
    public class Login
    {
        [Display(Name = "שם משתמש")]
        public string UserName { get; set; }

        [Display(Name = "סיסמא")]
        public string Password { get; set; }
    }
}