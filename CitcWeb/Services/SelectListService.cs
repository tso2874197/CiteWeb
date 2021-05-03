using System;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class SelectListService : ISelectListService
    {
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IClassInfoRepository _classRepository;

        public SelectListService(IBookInfoRepository bookInfoRepository, IClassInfoRepository classRepository)
        {
            _bookInfoRepository = bookInfoRepository;
            _classRepository = classRepository;
        }

        public SelectList GetBookInfo()
        {
            var bookInfos = _bookInfoRepository.Get(x=>x.IsBorrowed==false);
            return new SelectList(bookInfos, "Sn", "BookName");

        }

        public SelectList GetClassInfo()
        {
            var today = DateTime.Today;
            var classInfos = _classRepository.Get(x => x.EndDate > today);
            return new SelectList(classInfos, "Sn", "ClassName");
        }
    }
}