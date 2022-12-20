using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Course
{
    public class CreateCourseModel : PageModel
    {

        [BindProperty]
        public Domain.Entities.Course.Course Course { get; set; }

        public void OnGet()
        {
        }
    }
}
