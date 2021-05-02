using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class BorrowHistoryRepository:BaseRepository<BorrowHistory>, IBorrowHistoryRepository
    {
        public BorrowHistoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}