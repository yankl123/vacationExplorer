using Dache.DP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dache.PL.Models
{
    public class UserData : Supplier
    {
        [Display(Name = "הלוגו שלך, בחר תמונה ")]
        public IFormFile LogoImage { get; set; }
    }
}
