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
    }
}