using EP.Core.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Group
{
    public class DeleteGroupModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public DeleteGroupModel(ICourseServices courseServices)
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
            _courseServices.DeleteCourseGroup(Group);

            return RedirectToPage("Index");
        }
    }
}