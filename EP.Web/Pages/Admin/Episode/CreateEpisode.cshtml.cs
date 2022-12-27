using EP.Core.Interfaces.Course;
using EP.Core.Interfaces.Episode;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Episode
{
    [PermissionChecker(17)]
    public class CreateEpisodeModel : PageModel
    {
        private readonly IEpisodeServices _episodeServices;
        private readonly ICourseServices _courseServices;

        public CreateEpisodeModel(IEpisodeServices episodeServices, ICourseServices courseServices)
        {
            _episodeServices = episodeServices;
            _courseServices = courseServices;
        }

        [BindProperty]
        public Domain.Entities.Course.CourseEpisode Episode { get; set; }

        public IActionResult OnGet(int courseId)
        {

            if (courseId == 0 || !_courseServices.IsCourseExist(courseId))
            {
                return RedirectToPage("../Course/Index");
            }

            ViewData["CourseId"] = courseId;

            return Page();
        }

        public IActionResult OnPost(int courseId, IFormFile episodeFile)
        {
            _episodeServices.CreateEpisode(courseId,episodeFile,Episode);
            return RedirectToPage("Index", new { courseId = Episode.CourseId});
        }
    }
}
