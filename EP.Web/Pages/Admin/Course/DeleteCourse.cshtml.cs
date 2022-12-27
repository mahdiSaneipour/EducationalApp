using EP.Core.Interfaces.Course;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Course
{
    [PermissionChecker(15)]
    public class DeleteCourseModel : PageModel
    {

        private readonly ICourseServices _courseServices;

        public DeleteCourseModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.Course Course { get; set; }

        public void OnGet(int courseId)
        {
            Course = _courseServices.GetCourseByCourseId(courseId);
        }

        public IActionResult OnPost()
        {

            if (_courseServices.DeleteCourse(Course))
            {
                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
