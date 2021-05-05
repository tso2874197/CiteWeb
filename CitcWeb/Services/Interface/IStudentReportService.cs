using System.Collections;
using System.Collections.Generic;
using CitcWeb.Domain;

namespace CitcWeb.Services.Interface
{
    public interface IStudentReportService
    {
        IEnumerable<StudentReport> Get();
        void Add(StudentReport studentReport);
        StudentReport GetById(int id);
        void Update(StudentReport studentReport);
        void Delete(StudentReport studentReport);
        IEnumerable<StudentReport> Get(string studentName, string reportTitle, string className);
    }
}