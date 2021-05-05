using System;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class StudentReportsController : Controller
    {
        private CitcEntities db = new CitcEntities();
        private readonly IStudentReportService _studentReportService;
        private readonly ISelectListService _selectListService;

        public StudentReportsController(IStudentReportService studentReportService, ISelectListService selectListService)
        {
            _studentReportService = studentReportService;
            _selectListService = selectListService;
        }

        // GET: StudentReports
        public ActionResult Index(string studentName,string reportTitle,string className,int page = 1)
        {
            ViewBag.studentName = studentName;
            ViewBag.reportTitle = reportTitle;
            ViewBag.className = className;
            var studentReports = _studentReportService.Get(studentName,reportTitle,className);
            return View(studentReports.ToPagedList(page, 3));
        }


        // GET: StudentReports/Create
        public ActionResult Create()
        {
            PrepareSelectList();
            return View();
        }

        private void PrepareSelectList()
        {
            ViewBag.ClassSn = _selectListService.GetAllClassInfo();
        }

        // POST: StudentReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sn,StudentName,StudentNo,ReportTitle,UpdateTime,PdfPath,PptPath,ClassSn")] StudentReport studentReport, HttpPostedFileBase pdfFile, HttpPostedFileBase pptFile)
        {
            if (ModelState.IsValid)
            {
                studentReport.UpdateTime = DateTime.Now;
                if (pdfFile != null && pdfFile.ContentLength > 0)
                {
                    studentReport.PdfPath = SaveFile(pdfFile, studentReport.ClassSn.ToString());
                }

                if (pptFile != null && pptFile.ContentLength > 0)
                {
                    studentReport.PptPath = SaveFile(pptFile, studentReport.ClassSn.ToString());
                }

                _studentReportService.Add(studentReport);
                return RedirectToAction("Index");
            }
            PrepareSelectList();
            return View(studentReport);
        }

        private string SaveFile(HttpPostedFileBase file, string classSn)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Server.MapPath($"~/Upload/StudentReports/{classSn}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, fileName);
            file.SaveAs(path);
            fileName = $"{classSn}\\{Path.GetFileName(file.FileName)}";
            return fileName;
        }


        // GET: StudentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentReport = _studentReportService.GetById(id.Value);
            if (studentReport == null)
            {
                return HttpNotFound();
            }
            PrepareSelectList();
            return View(studentReport);
        }

        // POST: StudentReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,StudentName,StudentNo,ReportTitle,PdfPath,PptPath,ClassSn")] StudentReport studentReport, HttpPostedFileBase pdfFile, HttpPostedFileBase pptFile)
        {
            if (ModelState.IsValid)
            {
                studentReport.UpdateTime = DateTime.Now;
                string oldPdfPath = studentReport.PdfPath;
                string oldPptPath = studentReport.PptPath;
                if (pdfFile != null && pdfFile.ContentLength > 0)
                {
                    studentReport.PdfPath = SaveFile(pdfFile, studentReport.ClassSn.ToString());
                    DeleteFile(oldPdfPath);
                }

                if (pptFile != null && pptFile.ContentLength > 0)
                {
                    studentReport.PptPath = SaveFile(pptFile, studentReport.ClassSn.ToString());
                    DeleteFile(oldPptPath);
                }
                _studentReportService.Update(studentReport);

                return RedirectToAction("Index");
            }
            PrepareSelectList();
            return View(studentReport);
        }

        // GET: StudentReports/Delete/5
        public ActionResult Delete(int id)
        {
            var studentReport = _studentReportService.GetById(id);
            DeleteFile(studentReport.PdfPath);
            DeleteFile(studentReport.PptPath);
            _studentReportService.Delete(studentReport);
            return Redirect(Request.UrlReferrer?.ToString());
        }
        private void DeleteFile(string filePath)
        {
            var path = Server.MapPath($"~/Upload/StudentReports/{filePath}");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

    }
}
