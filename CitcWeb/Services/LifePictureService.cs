using System.Collections.Generic;
using System.Linq;
using CitcWeb.Domain;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services.Interface;

namespace CitcWeb.Services
{
    internal class LifePictureService : ILifePictureService
    {
        private readonly ILifePictureRepository _lifePictureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LifePictureService(ILifePictureRepository lifePictureRepository, IUnitOfWork unitOfWork)
        {
            _lifePictureRepository = lifePictureRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(LifePicture lifePicture)
        {
            _lifePictureRepository.Add(lifePicture);
            _unitOfWork.Commit();
        }

        public IEnumerable<LifePicture> GetLast10()
        {
            return _lifePictureRepository.Get(x=>x.IsValid).OrderByDescending(x => x.Sn).Take(10);
        }

        public void Remove(int id)
        {
            _lifePictureRepository.Remove(x=>x.Sn==id);
            _unitOfWork.Commit();
        }

        public LifePicture GetById(int id)
        {
            return _lifePictureRepository.GetById(id);
        }

        public void ShowToggle(int id)
        {
            var lifePicture = _lifePictureRepository.GetById(id);
            lifePicture.IsValid = lifePicture.IsValid != true;
            _unitOfWork.Commit();
        }

        public IEnumerable<LifePicture> Get()
        {
            return _lifePictureRepository.Get().OrderByDescending(x => x.Sn);
        }
    }
}