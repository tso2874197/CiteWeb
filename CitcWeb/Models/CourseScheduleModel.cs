using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CitcWeb.Domain;

namespace CitcWeb.Models
{
    public class CourseScheduleModel
    {
        public int Week { get; set; }
        public string ClassName { get; set; }
        public IEnumerable<CourseSchedule> CourseSchedules { get; set; }
        public int Month { get; set; }
        public int ClassSn { get; set; }
        public Dictionary<int, int> CourseHourLookup { get; set; }
    }
}