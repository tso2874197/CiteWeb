using System.Threading;
using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class CourseRepository:BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}