using System.Collections.Generic;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Models;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Teacher> Get()
        {
            return _teacherRepository.Get().OrderByDescending(x => x.Sn);
        }

        public void Add(IEnumerable<TeacherCsvModel> teacherCsvModels)
        {
            var teacherList = new List<Teacher>();
            foreach (var csvModel in teacherCsvModels)
            {
               teacherList.Add(new Teacher
               {
                    IdNum =csvModel.IdNum,
                     Name = csvModel.Name,
                     County = csvModel.County,
                     PhoneNum = csvModel.PhoneNum,
                     MilNum = csvModel.MilNum,
                     Email = csvModel.Email,
                     PayBureauNum = csvModel.PayBureauNum,
                     PayAccount = csvModel.PayAccount,
                     IsValid = true
               }); 
            }
            _teacherRepository.AddRange(teacherList);
            _unitOfWork.Commit();
        }

        public IEnumerable<Teacher> Get(string teacherName)
        {
            return _teacherRepository.Get(x=>x.Name.Contains(teacherName)).OrderByDescending(x=>x.Sn);
        }

        public Teacher GetById(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public void Update(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
            _unitOfWork.Commit();
        }

        public void TryDelete(int id)
        {
            var teacher = _teacherRepository.GetById(id);
            //if (teacher..Any())
            //{
            //    return;
            //}
            _teacherRepository.Remove(teacher);
            _unitOfWork.Commit();
        }
    }
}