using Dache.DAL;
using Dache.DP;
using Dache.PL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dache.PL.Controllers
{
    public class DacheController : Controller
    {
        // GET: DacheController
        private readonly IWebHostEnvironment _hostEnvironment;
        public DacheController( IWebHostEnvironment hostEnvironment)
        {
            
            this._hostEnvironment = hostEnvironment;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: DacheController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: DacheController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DacheController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(ServiceData Newservice)
        {
            try
            {
                Service service = Newservice;       
                if (Newservice.ShowImage != null)
                {
                    string wwwroot = _hostEnvironment.WebRootPath;
                    var uniqueFileName = MyTools.GetUniqueFileName(Newservice.ShowImage.FileName);
                    string filePath = @$"{wwwroot}/Images/{uniqueFileName}";
                    Newservice.ShowImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    Newservice.ImgUrl = @$"/Images/{uniqueFileName}";
                }
                BL.ChackActions.Insert<Service>(Newservice, "Service");
                return "OK";
            }
            catch (Exception)
            {
                return "NOTOK";
            }
        }

        // GET: DacheController/Edit/5
        public IActionResult Edit(int id)
        {
            List<Service> service = BL.ChackActions.Getinfobyid(id);
            var obj = service[0];
            var stringProperties = obj.GetType().GetProperties()
                          .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(obj, null);
                stringProperty.SetValue(obj, currentValue.Trim(), null);
            }
            ViewBag.curentvalues = obj;
            return View();
        }

        // POST: DacheController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(int id, ServiceData serviceData)
        {
            serviceData.ImgUrl = "null";
            try
            {
                if (serviceData.ShowImage != null)
                {
                    string wwwroot = _hostEnvironment.WebRootPath;
                    var uniqueFileName = MyTools.GetUniqueFileName(serviceData.ShowImage.FileName);
                    string filePath = @$"{wwwroot}/Images/{uniqueFileName}";
                    serviceData.ShowImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    serviceData.ImgUrl = @$"/Images/{uniqueFileName}";
                }
                Service Chservice = serviceData;
                BL.ChackActions.Update<Service>(Chservice, "Service", id);
                return $"OK";
            }
            catch (Exception)
            {
                return "NOTOK";
                throw;
            }

           
        }

        // GET: DacheController/Delete/5
        public string DeleteService(int id)
        {
            string response = BL.ChackActions.Delete("Service", id);
            return response;
        }

        // POST: DacheController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
