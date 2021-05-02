using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Models;
using CitcWeb.Models.Csv;
using CitcWeb.Services;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ActionResult Index(string courseName, int page = 1)
        {
            ViewBag.courseName = courseName;
            IEnumerable<Course> courses;
            if (string.IsNullOrWhiteSpace(courseName))
            {
                courses = _courseService.Get();
            }
            else
            {
                courses = _courseService.Get(courseName);
            }

            return View(courses.ToPagedList(page, 3));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "file")] HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var csvReader = new CsvReader<CourseCsvModel>();
                var courses = csvReader.Read(file.InputStream, true);
                _courseService.Add(courses);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = _courseService.GetById(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,Name,IsExist")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Update(course);
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public ActionResult Delete(int id)
        {
            _courseService.TryDelete(id);
            return Redirect(Request.UrlReferrer?.ToString());
        }
    }
}