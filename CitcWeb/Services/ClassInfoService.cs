using System;
using System.Collections.Generic;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Models;
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

        public void Add(IEnumerable<ClassCsvModel> classInfo)
        {
            var classInfos = new List<ClassInfo>();
            foreach (var csvModel in classInfo)
            {
                classInfos.Add(new ClassInfo
                {
                    ClassName = csvModel.ClassName,
                    StartDate = DateTime.Parse(csvModel.StartDate),
                    EndDate = DateTime.Parse(csvModel.EndDate)
                }); ;
            }
            _classInfoRepository.AddRange(classInfos);
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

        public IEnumerable<ClassInfo> Get(string className)
        {
            return _classInfoRepository.Get(x => x.ClassName.Contains(className)).OrderByDescending(x => x.Sn);
        }

        public void TryDelete(int id)
        {
            var classInfo = _classInfoRepository.GetById(id);
            if (classInfo.AnnualCourse.Any()||classInfo.BorrowHistory.Any()||classInfo.StudentReport.Any())
            {
                return;
            }
            _classInfoRepository.Remove(classInfo);
            _unitOfWork.Commit();
        }
    }
}