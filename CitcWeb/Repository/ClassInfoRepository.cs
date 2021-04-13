using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class ClassInfoRepository:BaseRepository<ClassInfoRepository>, IClassInfo
    {
        public ClassInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}