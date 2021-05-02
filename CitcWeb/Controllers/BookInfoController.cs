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
    public class BookInfoController : Controller
    {
        private CitcEntities db = new CitcEntities();
        private readonly IBookService _bookService;

        public BookInfoController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: BookInfo
        public ActionResult Index(string bookName,string bookNumber,int page=1)
        {
            ViewBag.bookName = bookName;
            ViewBag.bookNumber = bookNumber;
            IEnumerable<BookInfo> books;
            if (string.IsNullOrWhiteSpace(bookName)&&string.IsNullOrWhiteSpace(bookNumber))
            {
                books = _bookService.Get();
            }
            else
            {
                books = _bookService.Get(bookName,bookNumber);
            }

            return View(books.ToPagedList(page, 3));
        }

        // GET: BookInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "file")] HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var csvReader = new CsvReader<BookCsvModel>();
                var bookCsvModels = csvReader.Read(file.InputStream, true);
                _bookService.Add(bookCsvModels);
                
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: BookInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookInfo = _bookService.GetById(id.Value);
            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: BookInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,BookName,BookNumber")] BookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                _bookService.Update(bookInfo);
                return RedirectToAction("Index");
            }
            return View(bookInfo);
        }

        // GET: BookInfo/Delete/5
        public ActionResult Delete(int id)
        {
            _bookService.TryDelete(id);
            return Redirect(Request.UrlReferrer?.ToString());
        }

    }
}
