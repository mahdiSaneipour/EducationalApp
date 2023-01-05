using EP.Core.Interfaces.Course;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EP.Web.Pages.Admin.Course
{
    [PermissionChecker(13)]
    public class CreateCourseModel : PageModel
    {

        private readonly ICourseServices _courseServices;

        public CreateCourseModel(ICourseServices courseServices)
        {
            _courseServices= courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.Course Course { get; set; }

        public void OnGet()
        {
            Course = new Domain.Entities.Course.Course() {
                CourseImage = "Default.jpg"
            };

            SelectList groups = _courseServices.GetAllMainCourseGroupsAsSelectList();
            ViewData["Groups"] = groups;

            SelectList subGroups = _courseServices.GetCourseGroupsByParentIdAsSelectList(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = subGroups;

            SelectList statuses = _courseServices.GetAllCourseStatusesAsSelectList();
            ViewData["Statuses"] = statuses;

            SelectList levels = _courseServices.GetAllCourseLevelsAsSelectList();
            ViewData["Levels"] = levels;

            SelectList teachers = _courseServices.GetAllTeachersAsSelectList();
            ViewData["Teachers"] = teachers;

        }

        public IActionResult OnPost(IFormFile? courseDemo)
        {

            _courseServices.AddCourse(Course, courseDemo);

            return RedirectToPage("Index");
        }
    }
}
