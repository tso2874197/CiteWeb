using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Services.Interface;

namespace CitcWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILifePictureService _lifePictureService;

        public HomeController(ILifePictureService lifePictureService)
        {
            _lifePictureService = lifePictureService;
        }

        public ActionResult Index()
        {
            return View(_lifePictureService.GetLast10());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}