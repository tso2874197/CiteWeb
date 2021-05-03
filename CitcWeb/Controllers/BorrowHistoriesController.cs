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
using PagedList;

namespace CitcWeb.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private readonly ISelectListService _selectListService;
        private readonly IBookService _bookService;

        public BorrowHistoriesController(ISelectListService selectListService, IBookService bookService)
        {
            _selectListService = selectListService;
            _bookService = bookService;
        }

        // GET: BorrowHistories
        public ActionResult Index(int bookInfoSn, int page=1)
        {
            ViewBag.bookInfoSn = bookInfoSn;
            return View(_bookService.GetHistoryById(bookInfoSn).ToPagedList(page,3));
        }

        // GET: BorrowHistories/Create
        public ActionResult Create(int bookInfoSn)
        {
            GetSelectList();
            return View(new BorrowHistory{BookInfoSn = bookInfoSn});
        }

        private void GetSelectList()
        {
            ViewBag.BookInfoSn = _selectListService.GetBookInfo();
            ViewBag.ClassInfoSn = _selectListService.GetClassInfo();
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
                _bookService.Borrow(borrowHistory);
                return RedirectToAction("Index","BookInfo");
            }
            GetSelectList();
            return View(borrowHistory);
        }

        public ActionResult ReturnBook(int bookInfoSn)
        {
            _bookService.ReturnBook(bookInfoSn);
            return Redirect(Request.UrlReferrer?.ToString());
        }


    }
}
