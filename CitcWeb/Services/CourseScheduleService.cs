using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Models;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;
using Microsoft.Ajax.Utilities;

namespace CitcWeb.Services
{
    internal class CourseScheduleService : ICourseScheduleService
    {
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly IClassInfoRepository _classRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnualCourseRepository _annualCourseRepository;

        public CourseScheduleService(ICourseScheduleRepository courseScheduleRepository, ICourseRepository courseRepository, IClassInfoRepository classRepository, IUnitOfWork unitOfWork, IAnnualCourseRepository annualCourseRepository)
        {
            _courseScheduleRepository = courseScheduleRepository;
            _classRepository = classRepository;
            _unitOfWork = unitOfWork;
            _annualCourseRepository = annualCourseRepository;
        }

        public IEnumerable<CourseScheduleModel> Get(int classSn)
        {
            var classInfo = _classRepository.GetById(classSn);
            var courseSchedules = _courseScheduleRepository.Get(x=>x.ClassSn==classSn).ToList();
            var courseScheduleModels = new List<CourseScheduleModel>();
            var startMonday = GetMonday(classInfo.StartDate);
            var endFriday = GetMonday(classInfo.EndDate);
            var timeSpan = endFriday - startMonday;
            var weeks = timeSpan.Days / 7 + 1;
            var courseHourLookup = _annualCourseRepository.Get(x => x.ClassSn == classSn).ToDictionary(x => x.CourseSn, x => x.TotalHour);

            foreach (DateTime day in EachWeekDay(classInfo.StartDate, classInfo.EndDate))
            {
                for (var i = 0; i <= 10; i++)
                {
                    if (!courseSchedules.Any(x => x.Date==day && x.LessonStart<=i && x.LessonEnd>=i))
                    {
                        courseSchedules.Add(new CourseSchedule(){Date = day,LessonStart = i,LessonEnd =i,OtherEvent = "未排課"});
                    }
                }
            }
            courseSchedules = courseSchedules.OrderBy(x => x.Date).ThenBy(x=>x.LessonStart).ToList();
            for (int w = 1; w <= weeks; w++)
            {
                var week = w;
                courseScheduleModels.Add(new CourseScheduleModel
                {
                    Month = startMonday.AddDays((week-1)*7).Month,
                    Week = w,
                    ClassName = classInfo.ClassName,
                    ClassSn = classSn,
                    CourseHourLookup = courseHourLookup,
                    CourseSchedules = courseSchedules.Where(x=>startMonday.AddDays(week*7-1)> x.Date && x.Date> startMonday.AddDays((week-1)*7-1))
                });
            }
            return courseScheduleModels;
        }

        public string Add(CourseSchedule courseSchedule, bool confirmError)
        {
            if (!courseSchedule.OtherEvent.IsNullOrWhiteSpace())
            {
                courseSchedule.CourseSn = null;
                courseSchedule.TeacherSn = null;
                _courseScheduleRepository.Add(courseSchedule);
                _unitOfWork.Commit();
                return null;
            }
            if (courseSchedule.CourseSn==null)
            {
                return "未選擇課程";
            }
            if (courseSchedule.TeacherSn == null)
            {
                return "未選擇教官";
            }

            var teacherThisMonthSchedule = _courseScheduleRepository.Get(x=>x.Date.Month == courseSchedule.Date.Month && x.TeacherSn == courseSchedule.TeacherSn);
            var teacherThisMonthHours = teacherThisMonthSchedule.Any()? teacherThisMonthSchedule.Sum(x => x.LessonEnd - x.LessonStart + 1):0;
            if (confirmError==false&&teacherThisMonthHours + courseSchedule.LessonEnd - courseSchedule.LessonStart + 1>16)
            {
                return $"提醒：新增此次課程後此教官本月上課時數將會超過16小時，無誤請再次新增";
            }


            var annualCourse = _annualCourseRepository.GetSingle(x => x.ClassSn == courseSchedule.ClassSn && x.CourseSn ==courseSchedule.CourseSn);
            var courseSchedules = _courseScheduleRepository.Get(x => x.ClassSn == courseSchedule.ClassSn && x.CourseSn == courseSchedule.CourseSn);
            var currentHours = courseSchedules.Any() ? courseSchedules.Sum(x => x.LessonEnd - x.LessonStart + 1) : 0;
            if (annualCourse.TotalHour >= currentHours + (courseSchedule.LessonEnd - courseSchedule.LessonStart + 1))
            {
                _courseScheduleRepository.Add(courseSchedule);
                _unitOfWork.Commit();
                return null;
            }
            else
            {
                return $"本課程剩下{annualCourse.TotalHour - currentHours}節課";
            }
            return "未預期錯誤";
            
        }

        public void Delete(int sn)
        {
            _courseScheduleRepository.Remove(x=>x.Sn==sn);
            _unitOfWork.Commit();
        }

        private IEnumerable<DateTime> EachWeekDay(DateTime startDate, DateTime endDate)
        {
            var startMonday = startDate;
            var endFriday = endDate;
            startMonday = GetMonday(startMonday);
            endFriday = GetFriday(endFriday);
            for (var day = startMonday.Date; day.Date <= endFriday.Date; day = day.AddDays(1))
            {
                if(day.DayOfWeek==DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                yield return day;
            }
        }

        private static DateTime GetFriday(DateTime endFriday)
        {
            while (endFriday.DayOfWeek != DayOfWeek.Friday)
            {
                endFriday = endFriday.AddDays(1);
            }

            return endFriday;
        }

        private static DateTime GetMonday(DateTime startMonday)
        {
            while (startMonday.DayOfWeek != DayOfWeek.Monday)
            {
                startMonday = startMonday.AddDays(-1);
            }

            return startMonday;
        }

        private IEnumerable<DateTime> EachDay(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}