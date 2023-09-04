using Dache.DP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dache.PL.Models
{
    public class ServiceData : Service
    {
        public IFormFile ShowImage { get; set; }
    }
}
