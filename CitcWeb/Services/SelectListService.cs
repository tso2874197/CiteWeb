using System;
using System.Linq;
using System.Web.Mvc;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class SelectListService : ISelectListService
    {
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IClassInfoRepository _classRepository;
        private readonly ICourseRepository _courseRepository;

        public SelectListService(IBookInfoRepository bookInfoRepository, IClassInfoRepository classRepository, ICourseRepository courseRepository)
        {
            _bookInfoRepository = bookInfoRepository;
            _classRepository = classRepository;
            _courseRepository = courseRepository;
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

        public SelectList GetCourse(int classSn)
        {
            //var courseSns = _annualCourseRepository.Get(x => x.ClassSn == classSn).Select(x=>x.CourseSn);
            //var courses = _courseRepository.Get(x => courseSns.Contains(x.Sn) == false && x.IsExist==true);
            var courses = _courseRepository.Get(x => x.IsExist);
            return new SelectList(courses, "Sn", "Name");
        }

        public SelectList GetAllClassInfo()
        {
            return new SelectList(_classRepository.Get().OrderByDescending(x=>x.Sn), "Sn", "ClassName");
        }
    }
}