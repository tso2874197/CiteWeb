using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Models;
using CitcWeb.Models.Csv;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class ClassInfoController : Controller
    {
        private readonly IClassInfoService _classInfoService;

        public ClassInfoController(IClassInfoService classInfoService)
        {
            _classInfoService = classInfoService;
        }

        // GET: ClassInfoes
        public ActionResult Index(string className,int page=1)
        {
            ViewBag.className = className;
            IEnumerable<ClassInfo> classInfo;
            if (string.IsNullOrWhiteSpace(className))
            {
                classInfo = _classInfoService.Get();
            }
            else
            {
                classInfo = _classInfoService.Get(className);
            }

            return View(classInfo.ToPagedList(page, 3));
        }


        // GET: ClassInfoes/Create
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
                var csvReader = new CsvReader<ClassCsvModel>();
                var classInfo = csvReader.Read(file.InputStream, true);
                _classInfoService.Add(classInfo);
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

            var classInfo = _classInfoService.GetById(id.Value);
            if (classInfo == null)
            {
                return HttpNotFound();
            }
            return View(classInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,ClassName,StartDate,EndDate")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                _classInfoService.Update(classInfo);
                return RedirectToAction("Index");
            }
            return View(classInfo);
        }

        public ActionResult Delete(int id)
        {
            _classInfoService.TryDelete(id);
            return Redirect(Request.UrlReferrer?.ToString());
        }
        public ActionResult Copy(int id)
        {
            _classInfoService.Copy(id);
            return Redirect(Request.UrlReferrer?.ToString());
        }

    }
}
