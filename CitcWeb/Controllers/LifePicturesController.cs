using System;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class LifePicturesController : Controller
    {
        private CitcEntities db = new CitcEntities();
        private readonly ILifePictureService _lifePictureService;

        public LifePicturesController(ILifePictureService lifePictureService)
        {
            _lifePictureService = lifePictureService;
        }

        // GET: LifePictures
        public ActionResult Index(int page = 1)
        {
            return View(_lifePictureService.Get().ToPagedList(page,3));
        }

        // GET: LifePictures/Create
        public ActionResult Create()
        {
            return View(new LifePicture(){IsValid = true});
        }

        // POST: LifePictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsValid,PictureInfo,file")] LifePicture lifePicture, HttpPostedFileBase file)
        {
            if (ModelState.IsValid==false)
                return View(lifePicture);
            if (file != null && file.ContentLength > 0)
            {
                lifePicture.UploadTime=DateTime.Now;
                lifePicture.PicturePath=SaveFile(file);

                _lifePictureService.Add(lifePicture);

                return RedirectToAction("Index");
            }

            return View(lifePicture);
        }

        private string SaveFile(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Server.MapPath($"~/Upload/LifePictures/{DateTime.Now:yyyyMM}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, fileName);
            file.SaveAs(path);
            fileName = $"{DateTime.Now:yyyyMM}\\{Path.GetFileName(file.FileName)}";
            return fileName;
        }

        [HttpGet]
        public ActionResult ShowToggle(int id)
        {
            _lifePictureService.ShowToggle(id);
            return RedirectToAction("Index");
        }


        // POST: LifePictures/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var lifePicture = _lifePictureService.GetById(id);
            var path = Server.MapPath($"~/Upload/LifePictures/{lifePicture.PicturePath}");
            if (System.IO.File.Exists(path))
            {
               System.IO.File.Delete(path); 
            }
            _lifePictureService.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
