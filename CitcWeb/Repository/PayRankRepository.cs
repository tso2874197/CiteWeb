using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;

namespace CitcWeb.Repository
{
    public class PayRankRepository:BaseRepository<PayRank>, IPayRankRepository
    {
        public PayRankRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}