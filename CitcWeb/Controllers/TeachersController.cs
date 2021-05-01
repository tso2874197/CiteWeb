using System.Collections.Generic;
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
    public class TeachersController : Controller
    {
        private readonly CitcEntities db = new CitcEntities();
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: Teachers
        public ActionResult Index(string teacherName,int page=1)
        {
            ViewBag.teacherName = teacherName;
            IEnumerable<Teacher> teachers;
            if (string.IsNullOrEmpty(teacherName))
            {
                teachers = _teacherService.Get();
            }
            else
            {
                teachers = _teacherService.Get(teacherName);
            }

            return View(teachers.ToPagedList(page, 3));
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "file")] HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var csvReader = new CsvReader<TeacherCsvModel>();
                var teacherCsvModels = csvReader.Read(file.InputStream, true);
                _teacherService.Add(teacherCsvModels);
                
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
            var teacher = _teacherService.GetById(id.Value);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Sn,IdNum,Name,County,PhoneNum,MilNum,Email,PayBureauNum,PayAccount,IsValid")]
            Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherService.Update(teacher);
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int id)
        {
            _teacherService.TryDelete(id);
            
            return Redirect(Request.UrlReferrer?.ToString());
        }
        
    }
}