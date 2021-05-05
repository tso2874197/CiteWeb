using System.Collections.Generic;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;
using Microsoft.Ajax.Utilities;

namespace CitcWeb.Services
{
    public class StudentReportService : IStudentReportService
    {
        private readonly IStudentReportRepository _studentReportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentReportService(IStudentReportRepository studentReportRepository, IUnitOfWork unitOfWork)
        {
            _studentReportRepository = studentReportRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StudentReport> Get()
        {
            return _studentReportRepository.Get().OrderByDescending(x => x.ClassSn).ThenBy(x => x.StudentNo);
        }

        public void Add(StudentReport studentReport)
        {
            _studentReportRepository.Add(studentReport);
            _unitOfWork.Commit();
        }

        public StudentReport GetById(int id)
        {
            return _studentReportRepository.GetById(id);
        }

        public void Update(StudentReport studentReport)
        {
            _studentReportRepository.Update(studentReport);
            _unitOfWork.Commit();
        }

        public void Delete(StudentReport studentReport)
        {
            _studentReportRepository.Remove(studentReport);
            _unitOfWork.Commit();
        }

        public IEnumerable<StudentReport> Get(string studentName, string reportTitle, string className)
        {
            var studentReports = _studentReportRepository.Get();
            if (studentName.IsNullOrWhiteSpace()==false)
            {
                studentReports = studentReports.Where(x => x.StudentName.Contains(studentName));
            }
            if (reportTitle.IsNullOrWhiteSpace()==false)
            {
                studentReports = studentReports.Where(x => x.ReportTitle.Contains(reportTitle));
            }
            if (className.IsNullOrWhiteSpace()==false)
            {
                studentReports = studentReports.Where(x => x.ClassInfo.ClassName.Contains(className));
            }

            return studentReports.OrderByDescending(x => x.Sn);

        }
    }
}