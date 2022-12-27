using EP.Core.Interfaces.Course;
using EP.Core.Interfaces.Episode;
using EP.Core.Tools.Security;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Episode
{
    [PermissionChecker(18)]
    public class EditEpisodeModel : PageModel
    {
        private readonly IEpisodeServices _episodeServices;

        public EditEpisodeModel(IEpisodeServices episodeServices)
        {
            _episodeServices = episodeServices;
        }

        [BindProperty]
        public Domain.Entities.Course.CourseEpisode Episode { get; set; }

        public void OnGet(int episodeId)
        {
            Episode = _episodeServices.GetEpisodeByEpisodeId(episodeId);
        }

        public IActionResult OnPost(IFormFile episodeFile)
        {

            _episodeServices.EditEpisode(episodeFile, Episode);

            return RedirectToPage("Index", new { courseId = Episode.CourseId });
        }
    }
}
