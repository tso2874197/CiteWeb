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

        public BookService(IBookInfoRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
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
    }
}