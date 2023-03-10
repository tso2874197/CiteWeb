using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Models;

namespace CitcWeb.Services.Interface
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> Get();
        void Add(IEnumerable<TeacherCsvModel> teacherCsvModels);
        IEnumerable<Teacher> Get(string teacherName);
        Teacher GetById(int id);
        void Update(Teacher teacher);
        void TryDelete(int id);
    }
}