using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CitcWeb.Domain;

namespace CitcWeb.Repository.Base
{
    public class UnitOfWork:IUnitOfWork
    {
        public DbContext Context { get; set; }

        public UnitOfWork()
        {
            Context = new DbModel();
            Context.Database.Log = s => Debug.WriteLine(s);
        }

        public bool Commit()
        {
            bool result;
            try
            {
                result = Context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine($"Entity of type {eve.Entry.Entity.GetType().Name} in state {eve.Entry.State} has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine($"-property: {ve.PropertyName} , Error: {ve.ErrorMessage}");
                    }
                }
                throw;
            }
            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}