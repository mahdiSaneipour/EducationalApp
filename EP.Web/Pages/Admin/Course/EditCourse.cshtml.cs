using EP.Core.Interfaces.Course;
using EP.Core.ServiceModels.Course;
using EP.Core.Tools.Security;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EP.Web.Pages.Admin.Course
{
    [PermissionChecker(14)]
    public class EditCourseModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public EditCourseModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.Course Course { get; set; }

        public void OnGet(int courseId)
        {

            EditCourseServiceModel info = _courseServices.GetCourseInformationForEdit(courseId);

            Course = info.Course;

            SelectList groups = info.Groups;
            ViewData["Groups"] = groups;

            SelectList subGroups = info.SubGroups;
            ViewData["SubGroups"] = subGroups;

            SelectList statuses = info.Statuses;
            ViewData["Statuses"] = statuses;

            SelectList levels = info.Levels;
            ViewData["Levels"] = levels;

            SelectList teachers = info.Teachers;
            ViewData["Teachers"] = teachers;

        }

        public IActionResult OnPost(IFormFile? courseDemo)
        {

            _courseServices.EditCourse(Course, courseDemo);

            return RedirectToPage("Index");
        }
    }
}
