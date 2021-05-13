using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class CourseSchedulesController : Controller
    {
        private readonly IClassInfoService _classService;
        private readonly ICourseScheduleService _courseScheduleService;
        private readonly ISelectListService _selectListService;

        public CourseSchedulesController(IClassInfoService classService, ICourseScheduleService courseScheduleService, ISelectListService selectListService)
        {
            _classService = classService;
            _courseScheduleService = courseScheduleService;
            _selectListService = selectListService;
        }

        // GET: CourseSchedules
        public ActionResult Index(int classSn)
        {
            var courseSchedules = _courseScheduleService.Get(classSn);

            return View(courseSchedules);
        }
        public ActionResult ClassIndex(string className, int page = 1)
        {
            ViewBag.className = className;
            IEnumerable<ClassInfo> classInfo;
            if (string.IsNullOrWhiteSpace(className))
            {
                classInfo = _classService.Get();
            }
            else
            {
                classInfo = _classService.Get(className);
            }

            return View(classInfo.ToPagedList(page, 20));
        }


        // GET: CourseSchedules/Create
        public ActionResult Create(DateTime date,int lesson,int classSn)
        {
            PrepareSelectList(classSn);
            return View(new CourseSchedule {Date = date,LessonStart = lesson,LessonEnd = lesson,ClassSn = classSn});
        }

        private void PrepareSelectList(int classSn)
        {
            ViewBag.CourseSn = _selectListService.GetLeftCourse(classSn);
            ViewBag.TeacherSn = _selectListService.GetTeacher();
            ViewBag.LessonStart = _selectListService.GetLessonStartEnd();
            ViewBag.LessonEnd = _selectListService.GetLessonStartEnd();
        }

        // POST: CourseSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sn,Date,LessonStart,LessonEnd,Location,ClassSn,CourseSn,TeacherSn,OtherEvent,confirmError")] CourseSchedule courseSchedule,bool confirmError=false)
        {
            if (ModelState.IsValid)
            {
                var result = _courseScheduleService.Add(courseSchedule,confirmError);
                if (result == null)
                {
                    return RedirectToAction("ClassIndex");
                }
                else if (result.Contains("本月上課時數將會超過16小時"))
                {
                    ViewBag.teacherError = result;
                }
                else
                {
                    ViewBag.addResult = result;
                }

            }


            PrepareSelectList(courseSchedule.ClassSn);
            return View(courseSchedule);
        }
        public ActionResult Delete(int Sn)
        {
            _courseScheduleService.Delete(Sn);
            return Redirect(Request.UrlReferrer?.ToString());
        }



    }
}
