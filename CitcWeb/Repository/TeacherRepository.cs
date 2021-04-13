using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class TeacherRepository:BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}