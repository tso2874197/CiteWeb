using System.Data.Entity;

namespace CitcWeb.Repository.Base
{
    public interface IUnitOfWork
    {
        DbContext Context { get; set; }
        bool Commit();
    }
}