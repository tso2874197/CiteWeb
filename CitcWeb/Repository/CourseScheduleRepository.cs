using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class CourseScheduleRepository:BaseRepository<CourseSchedule>, ICourseScheduleRepository
    {
        public CourseScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}