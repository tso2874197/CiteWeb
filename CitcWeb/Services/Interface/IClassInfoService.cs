using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Models;

namespace CitcWeb.Services.Interface
{
    public interface IClassInfoService
    {
        IEnumerable<ClassInfo> Get();
        void Add(IEnumerable<ClassCsvModel> classInfo);
        ClassInfo GetById(int id);
        void Update(ClassInfo classInfo);
        IEnumerable<ClassInfo> Get(string className);
        void TryDelete(int id);
        void Copy(int id);
    }
}