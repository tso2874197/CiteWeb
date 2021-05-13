using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class SelectListService : ISelectListService
    {
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IClassInfoRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAnnualCourseRepository _annualCourseRepository;
        public readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly ITeacherRepository _teacherRepository;

        public SelectListService(IBookInfoRepository bookInfoRepository, IClassInfoRepository classRepository, ICourseRepository courseRepository, IAnnualCourseRepository annualCourseRepository, ICourseScheduleRepository courseScheduleRepository, ITeacherRepository teacherRepository)
        {
            _bookInfoRepository = bookInfoRepository;
            _classRepository = classRepository;
            _courseRepository = courseRepository;
            _annualCourseRepository = annualCourseRepository;
            _courseScheduleRepository = courseScheduleRepository;
            _teacherRepository = teacherRepository;
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

        public SelectList GetCourse()
        {
            var courses = _courseRepository.Get(x => x.IsExist);
            return new SelectList(courses, "Sn", "Name");
        }
        public SelectList GetLeftCourse(int classSn)
        {
            
            var annualCourses = _annualCourseRepository.Get(x => x.ClassSn == classSn);
            var courseSchedules = _courseScheduleRepository.Get(x => x.ClassSn == classSn);
            List<Course> courses = new List<Course>();
            foreach (var annualCourse in annualCourses)
            {
                var schedules = courseSchedules.Where(x=>x.CourseSn==annualCourse.CourseSn);
                var currentHours = schedules.Any()?schedules.Sum(x=>x.LessonEnd-x.LessonStart+1):0;
                if (annualCourse.TotalHour>currentHours)
                {
                    courses.Add(annualCourse.Course);
                }
            }

            return new SelectList(courses, "Sn", "Name");
        }

        public SelectList GetTeacher()
        {
            var teachers = _teacherRepository.Get(x=>x.IsValid==true);
            return new SelectList(teachers, "Sn", "Name");
        }

        public SelectList GetAllClassInfo()
        {
            return new SelectList(_classRepository.Get().OrderByDescending(x=>x.Sn), "Sn", "ClassName");
        }

        public SelectList GetLessonStartEnd()
        {
            return new SelectList(new int[] { 0,1,2,3,4,5,6,7,8,9,10});
        }
    }
}