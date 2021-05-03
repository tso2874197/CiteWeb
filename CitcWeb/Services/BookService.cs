using System;
using System.Collections.Generic;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Models;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class BookService : IBookService
    {
        private readonly IBookInfoRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBorrowHistoryRepository _borrowHistoryRepository;

        public BookService(IBookInfoRepository bookRepository, IUnitOfWork unitOfWork, IBorrowHistoryRepository borrowHistoryRepository)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _borrowHistoryRepository = borrowHistoryRepository;
        }

        public IEnumerable<BookInfo> Get()
        {
            return _bookRepository.Get().OrderByDescending(x => x.Sn);
        }

        public void Add(IEnumerable<BookCsvModel> bookCsvModels)
        {
            var books = new List<BookInfo>();
            foreach (var csvModel in bookCsvModels)
            {
                books.Add(new BookInfo()
                {
                    BookName = csvModel.BookName,
                    BookNumber = csvModel.BookNumber
                });
            }

            _bookRepository.AddRange(books);
            _unitOfWork.Commit();
        }

        public BookInfo GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void Update(BookInfo bookInfo)
        {
            _bookRepository.Update(bookInfo);
            _unitOfWork.Commit();
        }

        public void TryDelete(int id)
        {
            var bookInfo = _bookRepository.GetById(id);
            if (bookInfo.BorrowHistory.Any())
            {
                return;
            }

            _bookRepository.Remove(bookInfo);
            _unitOfWork.Commit();
        }

        public IEnumerable<BookInfo> Get(string bookName, string bookNumber)
        {
            var bookInfos = _bookRepository.Get();
            if (!string.IsNullOrWhiteSpace(bookName))
            {
                bookInfos = bookInfos.Where(x => x.BookName.Contains(bookName));
            }

            if (!string.IsNullOrWhiteSpace(bookNumber))
            {
                bookInfos = bookInfos.Where(x => x.BookNumber.Contains(bookNumber));
            }

            bookInfos = bookInfos.OrderByDescending(x => x.Sn);
            return bookInfos;
        }

        public void Borrow(BorrowHistory borrowHistory)
        {
            var bookInfo = _bookRepository.GetById(borrowHistory.BookInfoSn);
            bookInfo.IsBorrowed = true;
            borrowHistory.BorrowTime=DateTime.Now;
            _borrowHistoryRepository.Add(borrowHistory);
            _unitOfWork.Commit();
        }

        public IEnumerable<BorrowHistory> GetHistoryById(int bookInfoSn)
        {
            return _borrowHistoryRepository.Get(x => x.BookInfoSn == bookInfoSn).OrderByDescending(x => x.Sn);
        }

        public void ReturnBook(int bookInfoSn)
        {
            var bookInfo = _bookRepository.GetById(bookInfoSn);
            bookInfo.IsBorrowed = false;
            var borrowHistories = _borrowHistoryRepository.Get(x=>x.BookInfoSn==bookInfoSn && x.ReturnTime.HasValue ==false);
            foreach (var borrowHistory in borrowHistories)
            {
                borrowHistory.ReturnTime = DateTime.Now;
            }
            _unitOfWork.Commit();
        }
    }
}