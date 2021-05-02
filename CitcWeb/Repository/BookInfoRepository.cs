using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class BookInfoRepository:BaseRepository<BookInfo>, IBookInfoRepository
    {
        public BookInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}