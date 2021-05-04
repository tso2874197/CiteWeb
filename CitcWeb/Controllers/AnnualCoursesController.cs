using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class AnnualCoursesController : Controller
    {
        private CitcEntities db = new CitcEntities();
        private readonly ICourseService _courseService;
        private readonly ISelectListService _selectListService;

        public AnnualCoursesController(ICourseService courseService, ISelectListService selectListService)
        {
            _courseService = courseService;
            _selectListService = selectListService;
        }

        // GET: AnnualCourses
        public ActionResult Index(int classSn,string courseName, int page=1)
        {
            ViewBag.classSn = classSn;
            ViewBag.courseName = courseName;
            return View(_courseService.GetAnnualCourse(classSn,courseName).ToPagedList(page,2));
        }

        // GET: AnnualCourses/Create
        public ActionResult Create(int classSn)
        {
            PrepareSelectList(classSn);
            return View(new AnnualCourse{ClassSn = classSn});
        }

        private void PrepareSelectList(int classSn)
        {
            ViewBag.ClassSn = _selectListService.GetClassInfo();
            ViewBag.CourseSn = _selectListService.GetCourse(classSn);
        }

        // POST: AnnualCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sn,CourseSn,ClassSn,TotalHour")] AnnualCourse annualCourse)
        {
            if (ModelState.IsValid)
            {
                _courseService.AddAnnualCourse(annualCourse);
                return RedirectToAction("Index",new {classSn = annualCourse.ClassSn});
            }
            PrepareSelectList(annualCourse.ClassSn);
            return View(annualCourse);
        }

        // GET: AnnualCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annualCourse = _courseService.GetAnnualCourseById(id.Value);
            if (annualCourse == null)
            {
                return HttpNotFound();
            }
            PrepareSelectList(annualCourse.ClassSn);
            return View(annualCourse);
        }

        // POST: AnnualCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,CourseSn,ClassSn,TotalHour")] AnnualCourse annualCourse)
        {
            if (ModelState.IsValid)
            {
                _courseService.UpdateAnnualCourse(annualCourse);
                return RedirectToAction("Index",new {classSn = annualCourse.ClassSn});
            }
            PrepareSelectList(annualCourse.ClassSn);
            return View(annualCourse);
        }

        // GET: AnnualCourses/Delete/5
        public ActionResult Delete(int id)
        {
            _courseService.TryDeleteAnnualCourse(id);
            
            return Redirect(Request.UrlReferrer?.ToString());
        }

    }
}
