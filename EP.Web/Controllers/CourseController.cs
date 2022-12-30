using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Enums.Course;
using EP.Core.Interfaces.Course;
using EP.Core.Interfaces.Order;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EP.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseServices _courseServices;
        private readonly IOrderServices _orderServices;

        public CourseController(ICourseServices courseServices, IOrderServices orderServices)
        {
            _courseServices = courseServices;
            _orderServices = orderServices;
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

        [Route("ShowCourse/{courseId}")]
        public IActionResult ShowCourse(int courseId)
        {

            Domain.Entities.Course.Course course = _courseServices.GetCourseByCourseIdForShowCourse(courseId);

            return View(course);
        }

        public IActionResult BuyCourse(int courseId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            int orderId = _orderServices.AddOrder(userId,courseId);

            return Redirect("~/UserPanel/Order/ShowOrder/" + orderId);
        }
    }
}
