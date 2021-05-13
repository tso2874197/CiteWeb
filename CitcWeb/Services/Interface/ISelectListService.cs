using System.Web.Mvc;

namespace CitcWeb.Services.Interface
{
    public interface ISelectListService
    {
        SelectList GetBookInfo();
        SelectList GetClassInfo();
        SelectList GetCourse();
        SelectList GetAllClassInfo();
        SelectList GetLessonStartEnd();
        SelectList GetLeftCourse(int classSn);
        SelectList GetTeacher();
    }
}