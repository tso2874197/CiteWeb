using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services;
using CitcWeb.Services.Interface;

namespace CitcWeb.Controllers
{
    public class ClassInfoController : Controller
    {
        private CitcEntities db = new CitcEntities();
        private readonly IClassInfoService _classInfoService;

        public ClassInfoController(IClassInfoService classInfoService)
        {
            _classInfoService = classInfoService;
        }

        // GET: ClassInfoes
        public ActionResult Index()
        {
            var classInfos = _classInfoService.Get();
            return View(classInfos);
        }


        // GET: ClassInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sn,ClassName,StartDate,EndDate")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                _classInfoService.Add(classInfo);
                return RedirectToAction("Index");
            }

            return View(classInfo);
        }

        // GET: ClassInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassInfo classInfo = db.ClassInfo.Find(id);
            if (classInfo == null)
            {
                return HttpNotFound();
            }
            return View(classInfo);
        }

        // POST: ClassInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,ClassName,StartDate,EndDate")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classInfo);
        }

        // GET: ClassInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassInfo classInfo = db.ClassInfo.Find(id);
            if (classInfo == null)
            {
                return HttpNotFound();
            }
            return View(classInfo);
        }

        // POST: ClassInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassInfo classInfo = db.ClassInfo.Find(id);
            db.ClassInfo.Remove(classInfo);
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
