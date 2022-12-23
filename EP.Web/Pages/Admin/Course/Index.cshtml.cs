using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Course;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Course
{
    [PermissionChecker(3)]
    public class IndexModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public IndexModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public List<CourseViewModel> Courses { get; set; }

        public void OnGet()
        {
            Courses = _courseServices.GetAllCoursesForAdmin();
        }
    }
}
