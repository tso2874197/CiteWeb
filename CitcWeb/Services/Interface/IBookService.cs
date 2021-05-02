using System.Collections;
using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Models;

namespace CitcWeb.Services.Interface
{
    public interface IBookService
    {
        IEnumerable<BookInfo> Get();
        void Add(IEnumerable<BookCsvModel> bookCsvModels);
        BookInfo GetById(int id);
        void Update(BookInfo bookInfo);
        void TryDelete(int id);
        IEnumerable<BookInfo> Get(string bookName, string bookNumber);
    }
}