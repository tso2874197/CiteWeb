using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class StudentReportRepository:BaseRepository<StudentReport>, IStudentReportRepository
    {
        public StudentReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}