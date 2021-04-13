using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class LifePictureRepository:BaseRepository<LifePicture>, ILifePictureRepository
    {
        public LifePictureRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}