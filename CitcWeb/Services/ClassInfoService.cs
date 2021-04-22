using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class ClassInfoService : IClassInfoService
    {
        private readonly IClassInfoRepository _classInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClassInfoService(IClassInfoRepository classInfoRepository, IUnitOfWork unitOfWork)
        {
            _classInfoRepository = classInfoRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ClassInfo> Get()
        {
            return _classInfoRepository.Get();
        }

        public void Add(ClassInfo classInfo)
        {
            _classInfoRepository.Add(classInfo);
            _unitOfWork.Commit();
        }
    }
}