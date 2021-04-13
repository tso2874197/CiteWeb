using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class ClassInfoRepositoryRepository:BaseRepository<ClassInfoRepositoryRepository>, IClassInfoRepository
    {
        public ClassInfoRepositoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}