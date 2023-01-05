using EP.Core.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Group
{
    public class IndexModel : PageModel
    {
        private readonly ICourseServices _courseServices;

        public IndexModel(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public List<Domain.Entities.Course.CourseGroup> Groups { get; set; }

        public void OnGet()
        {
            Groups = _courseServices.GetAllCourseGroups();
        }
    }
}