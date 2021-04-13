using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class AnnualCourseRepository:BaseRepository<AnnualCourse>, IAnnualCourseRepository
    {
        public AnnualCourseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}