using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitcWeb.Domain;

namespace CitcWeb.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private CitcEntities db = new CitcEntities();

        // GET: BorrowHistories
        public ActionResult Index()
        {
            var borrowHistory = db.BorrowHistory.Include(b => b.BookInfo).Include(b => b.ClassInfo);
            return View(borrowHistory.ToList());
        }

        // GET: BorrowHistories/Create
        public ActionResult Create()
        {
            ViewBag.BookInfoSn = new SelectList(db.BookInfo, "Sn", "BookName");
            ViewBag.ClassInfoSn = new SelectList(db.ClassInfo, "Sn", "ClassName");
            return View();
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sn,BookInfoSn,ClassInfoSn,StudentName,BorrowTime,ReturnTime")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.BorrowHistory.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookInfoSn = new SelectList(db.BookInfo, "Sn", "BookName", borrowHistory.BookInfoSn);
            ViewBag.ClassInfoSn = new SelectList(db.ClassInfo, "Sn", "ClassName", borrowHistory.ClassInfoSn);
            return View(borrowHistory);
        }


    }
}
