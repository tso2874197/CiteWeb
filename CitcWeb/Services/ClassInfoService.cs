﻿using System.Collections.Generic;
using System.Linq;
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
            return _classInfoRepository.Get().OrderByDescending(x=>x.Sn);
        }

        public void Add(ClassInfo classInfo)
        {
            _classInfoRepository.Add(classInfo);
            _unitOfWork.Commit();
        }

        public ClassInfo GetById(int id)
        {
            return _classInfoRepository.GetById(id);
        }

        public void Update(ClassInfo classInfo)
        {
            _classInfoRepository.Update(classInfo);
            _unitOfWork.Commit();
        }
    }
}