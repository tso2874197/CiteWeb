using System.Collections;
using System.Collections.Generic;
using CitcWeb.Domain;

namespace CitcWeb.Services.Interface
{
    public interface ILifePictureService
    {
        void Add(LifePicture lifePicture);
        IEnumerable<LifePicture> GetLast10();
    }
}