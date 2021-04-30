using System.Net;
using System.Web.Mvc;
using CitcWeb.Domain;
using CitcWeb.Services.Interface;
using PagedList;

namespace CitcWeb.Controllers
{
    public class ClassInfoController : Controller
    {
        private readonly IClassInfoService _classInfoService;

        public ClassInfoController(IClassInfoService classInfoService)
        {
            _classInfoService = classInfoService;
        }

        // GET: ClassInfoes
        public ActionResult Index(int page=1)
        {
            var classInfos = _classInfoService.Get().ToPagedList(page,10);
            return View(classInfos);
        }


        // GET: ClassInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassName,StartDate,EndDate")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                _classInfoService.Add(classInfo);
                return RedirectToAction("Index");
            }

            return View(classInfo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classInfo = _classInfoService.GetById(id.Value);
            if (classInfo == null)
            {
                return HttpNotFound();
            }
            return View(classInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sn,ClassName,StartDate,EndDate")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                _classInfoService.Update(classInfo);
                return RedirectToAction("Index");
            }
            return View(classInfo);
        }

    }
}
