using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services;
using CitcWeb.Services.Interface;

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
        public ActionResult Index()
        {
            return View(db.LifePicture.ToList());
        }

        // GET: LifePictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LifePicture lifePicture = db.LifePicture.Find(id);
            if (lifePicture == null)
            {
                return HttpNotFound();
            }
            return View(lifePicture);
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

        // GET: LifePictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LifePicture lifePicture = db.LifePicture.Find(id);
            if (lifePicture == null)
            {
                return HttpNotFound();
            }
            return View(lifePicture);
        }

        // POST: LifePictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,PicturePath,UploadTime,IsValid,PictureInfo")] LifePicture lifePicture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lifePicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lifePicture);
        }

        // GET: LifePictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LifePicture lifePicture = db.LifePicture.Find(id);
            if (lifePicture == null)
            {
                return HttpNotFound();
            }
            return View(lifePicture);
        }

        // POST: LifePictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LifePicture lifePicture = db.LifePicture.Find(id);
            db.LifePicture.Remove(lifePicture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
