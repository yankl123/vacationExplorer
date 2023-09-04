using Dache.DAL;
using Dache.DP;
using Dache.PL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dache.PL.Controllers
{
    public class HomeController : Controller        
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger , IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
        }
       
      
        public IActionResult Index()
        {       
            return View();
        }
        [Route("/Home/GetInfo")]
        [Route("/Home/GetInfo/{categorys}/{areas}")]
        [Route("/Home/GetInfo/{categorys}/{areas}/{searchStr}")]
        public IActionResult GetInfo()
        {
            string[] areas = { "צפון", "דרום", "מרכז", "אזור ירושלים" };
            string[] categors = { "", "", "", "" };
            Dictionary<string ,string> routvals =  Request.RouteValues.ToDictionary(x => x.Key, x => x.Value.ToString());
            List<Service> services = BL.ChackActions.GetInfo(routvals);
            return View(services);
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public string CheckUserName(Login login )
        {
            if (BL.ChackActions.CheckUserName(login))
                return "EXISTS";
            else
                return "NOTEXISTS";
        }

        [HttpPost]
        public IActionResult login(Login credentials)
        {
            string UserName = credentials.UserName;
            string Password = credentials.Password;
            UserHeader userHeader = BL.ChackActions.GetUserHeder(UserName, Password);
            ViewBag.Name = userHeader.Name;
            ViewBag.Id = userHeader.Id;
            ViewBag.LogoUrl = userHeader.LogoUrl;
            ViewBag.lastAccsesTime = userHeader.lastAccsesTime;
            return View("UserScope");
        }

        public IActionResult GetInfoForUser(int id)
        {
            List<Service> services =  BL.ChackActions.GetUserInfo(id);

            return View(services);
        }
        public IActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public string NewAccount(UserData RegisterData)
        {    
            try
            {
                Supplier supplier = RegisterData;
                supplier.LastAccsesTime = DateTime.Now;
                string wwwroot = _hostEnvironment.WebRootPath;
                //בודק אם משתמש קיים
                if(BL.ChackActions.CheckUserName(new Login {UserName = supplier.UserName})) { return "NOTOK"; };
                if (RegisterData.LogoImage != null)
                {
                    string uniqueFileName = MyTools.GetUniqueFileName(RegisterData.LogoImage.FileName);
                    string filePath = @$"{wwwroot}/Images/{uniqueFileName}";
                    RegisterData.LogoImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    supplier.LogoUrl = @$"/Images/{uniqueFileName}";
                }
                BL.ChackActions.Insert<Supplier>(supplier, "supplier");
                try
                {
                    string sub = "רישום לאתר דאצ'ק";
                    string body = MyTools.EditLetter(supplier);
                    MyTools.SendEmail(supplier.Email.Trim(),sub,body);
                }
                catch (Exception)
                {

                    throw;
                }
               

                return "OK";
            }
            catch (Exception)
            {
                return "NOTOK";
            }
     
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public string getmessage()
        {
            string to = Request.Form["email"];
            string name = Request.Form["name"];
            string sub = "פנייה לדאצק";
            string body = $"שלום {name} \n פנייתך התקבלה ותטופל בהקדם";
            try
            {
                MyTools.SendEmail("jjbhs17be@gmail.com", "פנייה", Request.Form["message"] + "\n " +  Request.Form["email"]);
                try
                {
                    MyTools.SendEmail(to, sub, body);
                }
                catch (Exception)
                {

                    throw;
                }

                return "OK";
            }

            catch (Exception)
            {
                return "ERROR";
                throw;
            }

          
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
    }
}
