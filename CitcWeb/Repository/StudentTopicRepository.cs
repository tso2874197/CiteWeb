using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class StudentTopicRepository:BaseRepository<StudentTopic>, IStudentTopicRepository
    {
        public StudentTopicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}