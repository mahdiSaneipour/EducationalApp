using EP.Core.Interfaces.Course;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Group
{
    public class CreateGroupModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public CreateGroupModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.CourseGroup Group { get; set; }

        public void OnGet(int? groupId)
        {
            Group = new CourseGroup() { ParentId = groupId };
        }

        public IActionResult OnPost()
        {
            _courseServices.AddCourseGroup(Group);
            return RedirectToPage("Index");
        }
    }
}
