using EP.Core.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Group
{
    public class EditGroupModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public EditGroupModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.CourseGroup Group { get; set; }

        public void OnGet(int groupId)
        {
            Group = _courseServices.GetCourseGroupByGroupId(groupId);
        }

        public IActionResult OnPost()
        {

            _courseServices.EditCourseGroup(Group);

            return RedirectToPage("Index");
        }
    }
}
