using System.Collections;
using System.Collections.Generic;
using CitcWeb.Domain;

namespace CitcWeb.Services.Interface
{
    public interface IClassInfoService
    {
        IEnumerable<ClassInfo> Get();
        void Add(ClassInfo classInfo);
        ClassInfo GetById(int id);
        void Update(ClassInfo classInfo);
    }
}