using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class ClassInfoRepository:BaseRepository<ClassInfo>, IClassInfoRepository
    {
        public ClassInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}