using System.Collections;
using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Models;

namespace CitcWeb.Services.Interface
{
    public interface ICourseService
    {
        IEnumerable<Course> Get();
        void Add(IEnumerable<CourseCsvModel> courses);
        Course GetById(int id);
        void Update(Course course);
        void TryDelete(int id);
        IEnumerable<Course> Get(string courseName);
        IEnumerable<AnnualCourse> GetAnnualCourse(int classSn, string courseName);
        void AddAnnualCourse(AnnualCourse annualCourse);
        void UpdateAnnualCourse(AnnualCourse annualCourse);
        AnnualCourse GetAnnualCourseById(int id);
        void TryDeleteAnnualCourse(int id);
    }
}