using System.Collections.Generic;
using CitcWeb.Domain;
using CitcWeb.Models;

namespace CitcWeb.Services.Interface
{
    public interface ICourseScheduleService
    {
        IEnumerable<CourseScheduleModel> Get(int classSn);
        string Add(CourseSchedule courseSchedule, bool confirmError);
        void Delete(int sn);
    }
}