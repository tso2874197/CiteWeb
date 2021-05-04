using System.Web.Mvc;

namespace CitcWeb.Services.Interface
{
    public interface ISelectListService
    {
        SelectList GetBookInfo();
        SelectList GetClassInfo();
        SelectList GetCourse(int classSn);
    }
}