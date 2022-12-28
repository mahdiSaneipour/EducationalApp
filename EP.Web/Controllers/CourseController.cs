using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Enums.Course;
using EP.Core.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;

namespace EP.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public IActionResult Index(int pageId = 1,
            BoxCourseOrderByEnum orderBy = BoxCourseOrderByEnum.CreateDate,
            BoxCourseGetTypeEnum getType = BoxCourseGetTypeEnum.All, int minimumPrice = 0,
            int maximumPrice = 0, string filter = "", List<int> courseGroups = null)
        {

            Tuple<List<BoxCourseViewModel>, int> result = _courseServices.GetAllCourseByFilter(pageId,orderBy,getType,minimumPrice, maximumPrice, filter,courseGroups,9);

            ViewData["CourseGroups"] = _courseServices.GetAllCourseGroups();
            ViewData["SCourseGroups"] = courseGroups;
            ViewData["MinimumPrice"] = minimumPrice;
            ViewData["MaximumPrice"] = maximumPrice;
            ViewData["Filter"] = filter;
            ViewData["OrderBy"] = orderBy;
            ViewData["GetType"] = getType;
            ViewData["PageId"] = pageId;
            return View(result);
        }
    }
}
