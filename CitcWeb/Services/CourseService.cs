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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> Get()
        {
            return _courseRepository.Get().OrderByDescending(x => x.Sn);
        }

        public void Add(IEnumerable<CourseCsvModel> courses)
        {
            var courseList = new List<Course>();
            foreach (var csvModel in courses)
            {
               courseList.Add(new Course{Name = csvModel.Name,IsExist = true}); 
            }
            _courseRepository.AddRange(courseList);
            _unitOfWork.Commit();
        }

        public Course GetById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void Update(Course course)
        {
            _courseRepository.Update(course);
            _unitOfWork.Commit();
        }

        public void TryDelete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course.AnnualCourse.Any())
            {
                return;
            }
            _courseRepository.Remove(course);
            _unitOfWork.Commit();
        }

        public IEnumerable<Course> Get(string courseName)
        {
            return _courseRepository.Get(x => x.Name.Contains(courseName)).OrderByDescending(x => x.Sn);
        }
    }
}